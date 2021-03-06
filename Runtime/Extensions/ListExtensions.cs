using System.Collections.Generic;

namespace ParthViradiya.Core
{
	public static class ListExtensions
	{
		public static void EraseSwap<T>(this List<T> list, int index)
		{
			var lastIndex = list.Count - 1;
			list[index] = list[lastIndex];
			list.RemoveAt(lastIndex);
		}
	}
}