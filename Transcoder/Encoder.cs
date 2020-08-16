using System;
using System.Linq;

namespace Transcoder
{
	public class Encoder
	{
		public static Encoder CSV = new Encoder() { FilePath = String.Empty };
		public static Encoder FFMPEG = new Encoder() { FilePath = @"tools\ffmpeg\ffmpeg.exe" };
		public static Encoder FFPROBE = new Encoder() { FilePath = @"tools\ffmpeg\ffprobe.exe" };
		public static Encoder QAAC = new Encoder() { FilePath = @"tools\qaac\qaac64.exe" };

		public String FilePath { get; protected set; }

		public Boolean IsEncodingRequired
		{
			get
			{
				return !(new Encoder[] { Encoder.FFPROBE, Encoder.CSV }.Contains(this));
			}
		}
	}
}
