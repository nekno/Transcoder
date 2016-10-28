using System;

namespace Transcoder
{
	public class Encoder
	{
		public static Encoder QAAC = new Encoder() { FilePath = @"tools\qaac\qaac64.exe" };
		public static Encoder FFMPEG = new Encoder() { FilePath = @"tools\ffmpeg\ffmpeg.exe" };

		public String FilePath { get; protected set; }
	}
}
