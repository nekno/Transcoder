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

			MediaInfo mediaInfo = null;

			try {
				mediaInfo = new MediaInfo();

				mediaInfo.Open(filePath);
				var audioStreams = mediaInfo.Count_Get(StreamKind.Audio);

				return audioStreams > 0;
			} finally {
				if (mediaInfo != null) {
					mediaInfo.Close();
				}
			}
		}

		public TranscoderFile(String filePath, String folderPath = null) {
			FilePath = filePath;

			if (filePath == null || folderPath == null) {
				Folder = String.Empty;
				return;
			}

			var srcFolderPath = filePath.Replace(Path.GetFileName(filePath), String.Empty);
			var srcFolderName = srcFolderPath.Replace(folderPath, String.Empty).TrimEnd(new char[] { Path.DirectorySeparatorChar });
			var destFolderName = Path.GetFileName(folderPath);
			var destRelativeFolderPath = destFolderName + srcFolderName;

			Folder = destRelativeFolderPath;
		}
	}
}
