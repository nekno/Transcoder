using System;

namespace Transcoder
{
	public interface IMediaSegment
	{
		String EndTime { get; }
		String Name { get; }
		Int32 Number { get; }
		String StartTime { get; }
	}
}
