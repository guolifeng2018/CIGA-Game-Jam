using System.Collections.Generic;
using UnityEngine;

namespace TC.Core
{
	public static class ListUtil
	{
		#region Create

		public static List<T> Create<T> (int count, T value)
		{
			var list = new List<T> (count);
			for (var i = 0; i < count; i++) {
				list.Add (value);
			}
			return list;
		}

		public static T[] CreateArray<T> (int count, T value)
		{
			var array = new T[count];
			for (var i = 0; i < count; i++) {
				array[i] = value;
			}
			return array;
		}

		#endregion


		#region Random

		public static T GetRandomItem<T> (IList<T> list)
		{
			if (list == null || list.Count <= 0) {
				return default(T);
			}

			return list[Random.Range (0, list.Count)];
		}

		#endregion
	}
}