﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MediaInfoLib;

namespace Transcoder
{
	public class TranscoderFile
	{
        #region Static Fields

        public static Type[] Types = new Type[] {
			Type.QTAAC,
			Type.QTALAC,
			Type.FFMPEG_ALAC,
            Type.FLAC,
            Type.MP3CBR,
            Type.MP3VBR,
            Type.WAV,
			Type.TracksCSV,
			Type.RegionsCSV,
			Type.SplitInput,
		};

        #endregion

        #region Public Properties
		
		public bool Done { get; set; }
		public String EndTime { get; set; }
		public String FileName
		{
			get
			{
				return Path.GetFileName(FilePath);
			}
		}
		public String FilePath { get; protected set; }
		public String Folder { get; protected set; }
        public StringBuilder Log { get; set; } = new StringBuilder();
		public bool RequiresDecoding { get; set; }
		public TranscoderFile SourceFile { get; protected set; }
		public String StartTime { get; set; }
		public StreamInfo Stream { get; protected set; }

        #endregion

        #region Static Methods

        public static bool IsTranscodableFile(String filePath) {
			using (var mediaInfo = new MediaInfo()) { 
				mediaInfo.Open(filePath);
				var audioStreams = mediaInfo.Count_Get(StreamKind.Audio);

				//var allInfo = new StringBuilder();
				//// Parameter 0 contains the count of parameters for this stream
				//var paramCount = Convert.ToInt32(mediaInfo.Get(StreamKind.Audio, 0, 0, InfoKind.Text));

				//for (int i = 0; i < paramCount; i++)
				//{
				//	var infoName = mediaInfo.Get(StreamKind.Audio, 0, i, InfoKind.Name);
				//	var info = mediaInfo.Get(StreamKind.Audio, 0, i, InfoKind.Text);
				//	allInfo.AppendFormat("{0}: {1} = {2}\n", i, infoName, info);
				//}

				//var completeInfo = allInfo.ToString();

				//// 74: Duration/String3 HH:MM:SS.MMM
				//var streamLength = mediaInfo.Get(StreamKind.Audio, 0, 74, InfoKind.Text);

				return audioStreams > 0;
			} 
		}

        #endregion

        #region Constructors

        public TranscoderFile(String filePath, String rootFolderPath = null) {
			FilePath = filePath;
			Stream = new StreamInfo(filePath);

			if (String.IsNullOrEmpty(filePath) || String.IsNullOrEmpty(rootFolderPath)) {
				Folder = String.Empty;
				return;
			}

			var srcFolderPath = filePath.Replace(Path.GetFileName(filePath), String.Empty);
			var srcSubfolderRelativePath = srcFolderPath.Replace(rootFolderPath, String.Empty).TrimEnd(new char[] { Path.DirectorySeparatorChar });
			var destFolderName = Path.GetFileName(rootFolderPath);
			var destSubfolderRelativePath = destFolderName + srcSubfolderRelativePath;

			Folder = destSubfolderRelativePath;
		}

		#endregion

        #region Public Methods

        public String BuildCommandLineArgs(Type encoderType, Int32 bitrate, String baseOutputFolder) {
            var args =  String.Format(
				encoderType.CommandLineArgs(RequiresDecoding), 
				SourceFile?.FilePath ?? FilePath, // 0
				encoderType.BitrateArgs(bitrate), // 1
                OutputFilePath(encoderType, baseOutputFolder), // 2
				encoderType.BitDepthArgs(Stream.BitDepth), // 3
				StartTime, // 4
				EndTime // 5
			);

            return args;
		}

		public TranscoderFile GetFile(Track track)
        {
			return new TranscoderFile(String.Format("{0:000} - {1}{2}", track.Number, GetSafeFileName(track.Name), Path.GetExtension(FileName)), Folder)
			{
				SourceFile = this,
				StartTime = track.StartTime,
				EndTime = track.EndTime
			};
		}

		public TranscoderFile GetFile(MatroskaChapter chapter)
		{
			return new TranscoderFile(String.Format("{0:000} - {1}{2}", chapter.Number, GetSafeFileName(chapter.Name), Path.GetExtension(FileName)), Folder)
			{
				SourceFile = this,
				StartTime = chapter.StartTime,
				EndTime = chapter.EndTime
			};
		}

		public String OutputFilePath(Type encoderType, String baseOutputFolder)
		{
			return Path.Combine(OutputFolderPath(baseOutputFolder), Path.ChangeExtension(FileName, encoderType.FileExtension));
		}

		public String OutputFolderPath(string baseOutputFolder)
        {
			return Path.Combine(baseOutputFolder, Folder);
		}

		public void ResetFile()
		{
			Done = false;
			RequiresDecoding = false;
		}

		#endregion

		#region Protected Methods

		String GetSafeFileName(String fileName)
        {
			var safeFileName = fileName;

			foreach (var chr in Path.GetInvalidFileNameChars())
            {
				safeFileName = safeFileName.Replace(chr, '-');
            }

			return safeFileName;
        }

		private Int32? indexOfClosingQuote(List<String> values, Int32 startIdx)
        {
			for (int i = startIdx; i < values.Count; i++)
            {
				if (values[i].EndsWith("\""))
                {
					return i;
                }
            }

			return null;
        }

		#endregion

		#region Sub Types

		public class Type
		{
			#region Static Fields

			public static Type FFMPEG_ALAC = new Type()
			{
				Name = "FFMPEG ALAC",
				Encoder = Encoder.FFMPEG,
				FileExtension = ".m4a",
				CommandLineArgsWithoutDecoding = "-i \"{0}\" -vn -c:a alac -movflags +faststart -metadata:s:a gapless_playback=2 -y \"{2}\""
			};

			public static Type FLAC = new Type() { 
				Name = "FLAC", 
				Encoder = Encoder.FFMPEG, 
				FileExtension = ".flac",
				CommandLineArgsWithoutDecoding = "-i \"{0}\" -vn -y \"{2}\""
			};

            public static Type MP3CBR = new Type()
            {
                Name = "MP3 (CBR)",
                Encoder = Encoder.FFMPEG,
                FileExtension = ".mp3",
                IsBitrateRequired = true,
                CommandLineArgsWithoutDecoding = "-i \"{0}\" -vn -c:a libmp3lame -b:a {1}k -y \"{2}\""
            };

            public static Type MP3VBR = new Type()
            {
                Name = "MP3 (VBR)",
                Encoder = Encoder.FFMPEG,
                FileExtension = ".mp3",
                IsBitrateRequired = true,
                CommandLineArgsWithoutDecoding = "-i \"{0}\" -vn -c:a libmp3lame {1} -y \"{2}\"",
                BitrateMap =
                {
                    { 64, "-q:a 9" },
                    { 96, "-q:a 7" },
                    { 128, "-q:a 5" },
                    { 192, "-q:a 2" },
                    { 224, "-q:a 1" },
                    { 256, "-q:a 0" },
                    { 320, "-b:a 320k" },
                }
            };

			public static Type QTAAC = new Type()
			{
				Name = "QuickTime AAC (CVBR)",
				Encoder = Encoder.QAAC,
				FileExtension = ".m4a",
				IsBitrateRequired = true,
				CommandLineArgsWithDecoding = "- --threading --gapless-mode 2 --copy-artwork -v{1} -o \"{2}\"",
				CommandLineArgsWithoutDecoding = "\"{0}\" --threading --gapless-mode 2 --copy-artwork -v{1} -o \"{2}\""
			};

			public static Type QTALAC = new Type()
			{
				Name = "QuickTime ALAC",
				Encoder = Encoder.QAAC,
				FileExtension = ".m4a",
				CommandLineArgsWithDecoding = "- --threading --gapless-mode 2 --copy-artwork -A -o \"{2}\"",
				CommandLineArgsWithoutDecoding = "\"{0}\" --threading --gapless-mode 2 --copy-artwork -A -o \"{2}\""
			};

			public static Type RegionsCSV = new Type()
			{
				Name = "Sound Forge Regions CSV",
				Encoder = Encoder.CSV,
				FileExtension = ".csv"
			};

			public static Type SplitInput = new Type()
			{
				Name = "Split Input File",
				Encoder = Encoder.FFMPEG,
				CommandLineArgsWithoutDecoding = "-i \"{0}\" -c copy -ss {4} -to {5} -y \"{2}\""
			};

			public static Type TracksCSV = new Type()
			{
				Name = "Tracks CSV",
				Encoder = Encoder.CSV,
				FileExtension = ".csv"
			};

			public static Type WAV = new Type() { 
				Name = "WAV",
				Encoder = Encoder.FFMPEG, 
				FileExtension = ".wav",
				CommandLineArgsWithoutDecoding = "-i \"{0}\" -vn -y {3} \"{2}\"",
				BitDepthMap =
				{
					{ 16, "-c:a pcm_s16le" },
					{ 24, "-c:a pcm_s24le" },
					{ 32, "-c:a pcm_s32le" }
				}
			};

            #endregion

            #region Public Properties

            public String Name { get; set; }
            public Encoder Encoder { get; set; }
            public String FileExtension { get; set; }
			public Boolean IsBitrateRequired { get; set; }

			protected Dictionary<Int32, String> BitDepthMap { get; set; } = new Dictionary<Int32, String>();
            protected Dictionary<Int32, String> BitrateMap { get; set; } = new Dictionary<Int32, String>();
            protected String CommandLineArgsWithDecoding { get; set; }
			protected String CommandLineArgsWithoutDecoding { get; set; }

			#endregion

			#region Public Methods

			public String BitDepthArgs(Int32? bitDepth)
			{
				if (bitDepth.HasValue && BitDepthMap.ContainsKey(bitDepth.Value))
				{
					return BitDepthMap[bitDepth.Value];
				}
				else
				{
					return String.Empty;
				}
			}

			public String BitrateArgs(Int32 bitrate)
            {
                if (BitrateMap.ContainsKey(bitrate))
                {
                    return BitrateMap[bitrate];
                }
                else
                {
                    return bitrate.ToString();
                }
            }

            public String CommandLineArgs(bool isDecodingRequired)
            {
				return isDecodingRequired ? CommandLineArgsWithDecoding : CommandLineArgsWithoutDecoding;
            }

            #endregion
        }

		public class StreamInfo
        {
			public Int32? BitDepth { get; protected set; }
			public String Duration { get; protected set; }
			public Int64? Samples { get; protected set; }
			public String Title { get; protected set; }

			public StreamInfo(String filePath)
            {
				if (!File.Exists(filePath))
					return;

				using (var decoder = new Process())
				{
					decoder.StartInfo = new ProcessStartInfo()
					{
						FileName = Path.Combine(Environment.CurrentDirectory, Encoder.FFPROBE.FilePath),
						Arguments = String.Format("-sexagesimal -select_streams a -show_entries stream=bits_per_raw_sample,duration,duration_ts:format_tags=title -of flat \"{0}\"", filePath),
						WindowStyle = ProcessWindowStyle.Hidden,
						CreateNoWindow = true,
						UseShellExecute = false,
						RedirectStandardInput = false,
						RedirectStandardOutput = true,
						RedirectStandardError = false,
						StandardOutputEncoding = Encoding.UTF8
					};

					decoder.Start();
					var output = decoder.StandardOutput.ReadToEnd();

					var bitDepthMatch = Regex.Match(output, "^streams.stream.0.bits_per_raw_sample=\"([\\d]+)\"\r?$", RegexOptions.Multiline);
					if (bitDepthMatch.Success && bitDepthMatch.Groups.Count == 2)
					{
						if (Int32.TryParse(bitDepthMatch.Groups[1].Value, out int bitValue))
						{
							BitDepth = bitValue;
						}
					}

					var durationMatch = Regex.Match(output, "^streams.stream.0.duration=\"(.+)\"\r?$", RegexOptions.Multiline);
					if (durationMatch.Success && durationMatch.Groups.Count == 2)
					{
						Duration = durationMatch.Groups[1].Value;
					}

					var samplesMatch = Regex.Match(output, "^streams.stream.0.duration_ts=([\\d]+)\r?$", RegexOptions.Multiline);
					if (samplesMatch.Success && samplesMatch.Groups.Count == 2)
					{
						if (Int32.TryParse(samplesMatch.Groups[1].Value, out int samplesValue))
						{
							Samples = samplesValue;
						}
					}

					var titleMatch = Regex.Match(output, "^format.tags.title=\"(.+)\"\r?$", RegexOptions.Multiline);
					if (titleMatch.Success && titleMatch.Groups.Count == 2)
					{
						Title = titleMatch.Groups[1].Value;
					}
				}
			}
        }

        #endregion
    }
}
