using System;
using System.Collections.Generic;

namespace TC.Core
{
	public static class DictionaryExtensions
	{
		public static TValue GetValue<TKey, TValue> (this Dictionary<TKey, TValue> dict, TKey key)
		{
			return DictionaryUtil.GetValue (dict, key);
		}

		public static TValue GetOrAddNewValue<TKey, TValue> (this Dictionary<TKey, TValue> dict, TKey key) where TValue : class, new ()
		{
			return DictionaryUtil.GetOrAddNewValue (dict, key);
		}

		public static TValue GetOrAddDefaultValue<TKey, TValue> (this Dictionary<TKey, TValue> dict, TKey key)
		{
			return DictionaryUtil.GetOrAddDefaultValue (dict, key);
		}

		public static void ForEach<TKey, TValue> (this Dictionary<TKey, TValue> dict, Action<TKey, TValue> action)
		{
			DictionaryUtil.ForEach (dict, action);

		}

		public static void ForEach<TKey, TValue> (this Dictionary<TKey, TValue> dict, Action<TValue> action)
		{
			DictionaryUtil.ForEach (dict, action);
		}
	}
}