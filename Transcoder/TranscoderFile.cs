using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public TranscoderFile(String filePath, String folderPath = null) {
			FilePath = filePath;

			if (filePath == null || folderPath == null) {
				Folder = String.Empty;
				return;
			}

			var srcFolderPath = filePath.Replace(Path.GetFileName(filePath), String.Empty);
			var srcFolderName = srcFolderPath.Replace(folderPath, String.Empty).TrimEnd(new char[] { '\\' });
			var destFolderName = Path.GetFileName(folderPath);
			var destRelativeFolderPath = Path.Combine(destFolderName, srcFolderName);

			Folder = destRelativeFolderPath;
		}
	}
}
