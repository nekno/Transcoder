using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Transcoder
{
	public class MatroskaChapter : IMediaSegment
	{
		public static String FileExtension = ".xml";
		public static String FileFilter = $"Matroska Chapters (*{FileExtension})|*{FileExtension}";

		public String EndTime { get; protected set; }
		public String Name { get; protected set; }
		public Int32 Number { get; protected set; }
		public String StartTime { get; protected set; }

		public MatroskaChapter(XElement chapterXml, Int32 number, String endTime)
		{
			Number = number;
			Name = chapterXml.Element("ChapterDisplay")?.Element("ChapterString")?.Value ?? $"Chapter {number:000}";
			StartTime = chapterXml.Element("ChapterTimeStart").Value;

			try
			{
				// Not every chapter will have an end time, so try to use the start time of the next chapter
				EndTime = chapterXml.Element("ChapterTimeEnd")?.Value
					?? chapterXml.ElementsAfterSelf().First().Element("ChapterTimeStart").Value;
			} 
			catch
			{
				// For the last chapter, use sthe end time of the file
				EndTime = endTime;
			}
		}

		public static IEnumerable<MatroskaChapter> GetChapters(String fileName, String endTime)
		{
			var chapters = XElement.Load(fileName).Descendants("ChapterAtom")
						   .Where(chapter => (chapter.Element("ChapterFlagEnabled")?.Value ?? "1") == "1")
						   .Where(chapter => (chapter.Element("ChapterFlagHidden")?.Value ?? "0") == "0")
						   .Select((chapter, index) => new MatroskaChapter(chapter, index + 1, endTime));
			return chapters;
		}
	}
}
