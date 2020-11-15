using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Transcoder
{
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
}
