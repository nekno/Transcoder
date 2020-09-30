using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Transcoder
{
    public class Track : IMediaSegment
	{
		public static String FileExtension = ".csv";
		public static String FileFilter = $"Comma Separated Values (*{FileExtension})|*{FileExtension}";
		public String EndTime { get; protected set; }
		public String Name { get; protected set; }
		public Int32 Number { get; protected set; }
		public String StartTime { get; protected set; }

		public Track(String csvLine)
        {
			var values = ParseCsv(csvLine);

			if (values.Count < 4)
				throw new FormatException();

			Number = Int32.Parse(values[0]);
			Name = values[1];
			StartTime = values[2];
			EndTime = values[3];
		}

		public static IEnumerable<Track> GetTracks(String fileName)
        {
			var tracks = from csvLine in File.ReadAllLines(fileName)
						 select new Track(csvLine);
			return tracks;
		}

		List<String> ParseCsv(String csvLine)
		{
			var values = new List<string>(csvLine.Split(','));
			var returnValues = new List<string>(values.Count);

			for (int i = 0; i < values.Count; i++)
			{
				string value = values[i];
				var countToHere = i + 1;

				if (value.StartsWith("\"") && countToHere < values.Count)
				{
					var endIdx = values.FindIndex(countToHere, v => v.EndsWith("\""));
					if (endIdx >= 0)
					{
						var subValues = values.GetRange(i, endIdx - i + 1);
						returnValues.Add(String.Join(",", subValues).Trim('\"'));
						i = endIdx;
					}
					else
					{
						returnValues.Add(value);
					}
				}
				else
				{
					returnValues.Add(value);
				}
			}

			return returnValues;
		}
    }
}
