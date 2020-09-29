using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
