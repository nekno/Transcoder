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
			Type.FLAC,
			Type.QTAAC,
			Type.QTALAC,
			Type.WAV
		};
		
		public String FilePath { get; protected set; }
		public String Folder { get; protected set; }
		public String Log { get; set; }
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

		public String BuildCommandLineArgs(Type encoderType, Int32 bitrate, String outputFolder) {
			return String.Format(
				encoderType.CommandLineArgs(RequiresDecoding), 
				FilePath, 
				bitrate,
				OutputFolderPath(outputFolder),
				Path.Combine(Path.Combine(outputFolder, Folder), Path.ChangeExtension(FileName, encoderType.FileExtension))
			);
		}

		public String OutputFolderPath(string baseOutputFolder) {
			return Path.Combine(baseOutputFolder, Folder);
		}

		public class Type
		{
			public static Type QTAAC = new Type() { 
				Name = "QuickTime AAC", 
				FileExtension = ".m4a", 
				Encoder = Transcoder.Encoder.QAAC, 
				IsBitrateRequired = true,
				CommandLineArgsWithDecoding = "- --threading --gapless-mode 2 -v{1} -o \"{3}\"",
				CommandLineArgsWithoutDecoding = "\"{0}\" --threading --gapless-mode 2 -v{1} -d \"{2}\""
			};
			public static Type QTALAC = new Type() { 
				Name = "QuickTime ALAC", 
				Encoder = Encoder.QAAC, 
				FileExtension = ".m4a",
				CommandLineArgsWithDecoding = "- --threading --gapless-mode 2 -A -o \"{3}\"",
				CommandLineArgsWithoutDecoding = "\"{0}\" --threading --gapless-mode 2 -A -d \"{2}\""
			};

			public static Type FLAC = new Type() { 
				Name = "FLAC", 
				Encoder = Encoder.FFMPEG, 
				FileExtension = ".flac",
				CommandLineArgsWithDecoding = String.Empty,
				CommandLineArgsWithoutDecoding = "-i \"{0}\" -y \"{3}\""
			};

			public static Type WAV = new Type() { 
				Name = "WAV",
				Encoder = Encoder.FFMPEG, 
				FileExtension = ".wav",
				CommandLineArgsWithDecoding = String.Empty,
				CommandLineArgsWithoutDecoding = "-i \"{0}\" -y \"{3}\""
			};

			public String Name { get; protected set; }
			public String FileExtension { get; protected set; }
			public Encoder Encoder { get; protected set; }
			public Boolean IsBitrateRequired { get; protected set; }

			protected String CommandLineArgsWithDecoding { get; set; }
			protected String CommandLineArgsWithoutDecoding { get; set; }

			public String CommandLineArgs(bool isDecodingRequired) {
				return isDecodingRequired ? CommandLineArgsWithDecoding : CommandLineArgsWithoutDecoding;
			}
		}
	}
}
