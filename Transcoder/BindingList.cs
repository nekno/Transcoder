using System.Collections.Generic;
using System.ComponentModel;

namespace Transcoder
{
	static class BindingListExtension
	{
		public static void Add<T>(this BindingList<T> @this, IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				@this.Add(item);
			}
		}
	}
}
