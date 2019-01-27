using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaInfoLib;

namespace Transcoder
{
	public class TranscoderFile
	{
		public static Type[] Types = new Type[] {
			Type.QTAAC,
			Type.QTALAC,
            Type.FLAC,
            Type.MP3CBR,
            Type.MP3VBR,
            Type.WAV
		};
		
		public String FilePath { get; protected set; }
		public String Folder { get; protected set; }
        public StringBuilder Log { get; set; } = new StringBuilder();
		public bool RequiresDecoding { get; set; }
		public bool Done { get; set; }

		public String FileName {
			get {
				return Path.GetFileName(FilePath);
			}
		}

		public static bool IsTranscodableFile(String filePath) {
			//using (var decoder = new Process()) {
			//	decoder.StartInfo = new ProcessStartInfo() {
			//		FileName = Path.Combine(Environment.CurrentDirectory, @"tools\ffmpeg\ffmpeg.exe"),
			//		Arguments = String.Format("-i \"{0}\" -acodec copy -v error -f null -", filePath),
			//		WindowStyle = ProcessWindowStyle.Hidden,
			//		CreateNoWindow = true,
			//		UseShellExecute = false,
			//		RedirectStandardInput = false,
			//		RedirectStandardOutput = false,
			//		RedirectStandardError = true
			//	};

			//	decoder.Start();
			//	var result = decoder.StandardError.ReadToEnd();

			//	return String.IsNullOrEmpty(result);
			//}

			using (var mediaInfo = new MediaInfo()) { 
				mediaInfo.Open(filePath);
				var audioStreams = mediaInfo.Count_Get(StreamKind.Audio);

				return audioStreams > 0;
			} 
		}

		public TranscoderFile(String filePath, String rootFolderPath = null) {
			FilePath = filePath;

			if (filePath == null || rootFolderPath == null) {
				Folder = String.Empty;
				return;
			}

			var srcFolderPath = filePath.Replace(Path.GetFileName(filePath), String.Empty);
			var srcSubfolderRelativePath = srcFolderPath.Replace(rootFolderPath, String.Empty).TrimEnd(new char[] { Path.DirectorySeparatorChar });
			var destFolderName = Path.GetFileName(rootFolderPath);
			var destSubfolderRelativePath = destFolderName + srcSubfolderRelativePath;

			Folder = destSubfolderRelativePath;
		}

		public String BuildCommandLineArgs(Type encoderType, Int32 bitrate, String baseOutputFolder) {
            var args =  String.Format(
				encoderType.CommandLineArgs(RequiresDecoding), 
				FilePath, 
				encoderType.BitrateArgs(bitrate),
                OutputFilePath(encoderType, baseOutputFolder)
			);

            return args;
		}

		public String OutputFolderPath(string baseOutputFolder)
        {
			return Path.Combine(baseOutputFolder, Folder);
		}

        public String OutputFilePath(Type encoderType, String baseOutputFolder)
        {
            return Path.Combine(OutputFolderPath(baseOutputFolder), Path.ChangeExtension(FileName, encoderType.FileExtension));
        }

        public class Type
		{
			public static Type QTAAC = new Type() { 
				Name = "QuickTime AAC (CVBR)",
                Encoder = Encoder.QAAC,
                FileExtension = ".m4a", 
				IsBitrateRequired = true,
				CommandLineArgsWithDecoding = "- --threading --gapless-mode 2 --copy-artwork -v{1} -o \"{2}\"",
				CommandLineArgsWithoutDecoding = "\"{0}\" --threading --gapless-mode 2 --copy-artwork -v{1} -o \"{2}\""
            };

			public static Type QTALAC = new Type() { 
				Name = "QuickTime ALAC", 
				Encoder = Encoder.QAAC, 
				FileExtension = ".m4a",
				CommandLineArgsWithDecoding = "- --threading --gapless-mode 2 --copy-artwork -A -o \"{2}\"",
				CommandLineArgsWithoutDecoding = "\"{0}\" --threading --gapless-mode 2 --copy-artwork -A -o \"{2}\""
            };

			public static Type FLAC = new Type() { 
				Name = "FLAC", 
				Encoder = Encoder.FFMPEG, 
				FileExtension = ".flac",
				CommandLineArgsWithDecoding = String.Empty,
				CommandLineArgsWithoutDecoding = "-i \"{0}\" -y \"{2}\""
			};

            public static Type MP3CBR = new Type()
            {
                Name = "MP3 (CBR)",
                Encoder = Encoder.FFMPEG,
                FileExtension = ".mp3",
                IsBitrateRequired = true,
                CommandLineArgsWithDecoding = String.Empty,
                CommandLineArgsWithoutDecoding = "-i \"{0}\" -c:a libmp3lame -b:a {1}k -y \"{2}\""
            };

            public static Type MP3VBR = new Type()
            {
                Name = "MP3 (VBR)",
                Encoder = Encoder.FFMPEG,
                FileExtension = ".mp3",
                IsBitrateRequired = true,
                CommandLineArgsWithDecoding = String.Empty,
                CommandLineArgsWithoutDecoding = "-i \"{0}\" -c:a libmp3lame {1} -y \"{2}\"",
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

            public static Type WAV = new Type() { 
				Name = "WAV",
				Encoder = Encoder.FFMPEG, 
				FileExtension = ".wav",
				CommandLineArgsWithDecoding = String.Empty,
				CommandLineArgsWithoutDecoding = "-i \"{0}\" -y \"{2}\""
			};

            public String Name { get; protected set; }
            public Encoder Encoder { get; protected set; }
            public String FileExtension { get; protected set; }
			public Boolean IsBitrateRequired { get; protected set; }

            protected Dictionary<Int32, String> BitrateMap { get; set; } = new Dictionary<Int32, String>();
            protected String CommandLineArgsWithDecoding { get; set; }
			protected String CommandLineArgsWithoutDecoding { get; set; }

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
		}
	}
}
