using System;
using System.Collections.Generic;

namespace TC.Core
{
	public static class DictionaryUtil
	{
		#region Create

		public static Dictionary<TKey, TValue> Create<TKey, TValue> (IList<TValue> values, Func<TValue, TKey> keyFunc)
		{
			if (values == null) {
				return null;
			}

			var dictionary = new Dictionary<TKey, TValue> (values.Count);
			foreach (var value in values) {
				var key = keyFunc (value);
				if (dictionary.ContainsKey (key)) {
					continue;
				}
				dictionary.Add (key, value);
			}

			return dictionary;
		}

		public static Dictionary<TKey, TValue> Create<TKey, TValue, TContent> (IList<TContent> contents, Func<TContent, TKey> keyFunc,
			Func<TContent, TValue> valueFunc)
		{
			if (contents == null) {
				return null;
			}

			var dictionary = new Dictionary<TKey, TValue> (contents.Count);
			foreach (var content in contents) {
				var key = keyFunc (content);
				if (dictionary.ContainsKey (key)) {
					continue;
				}
				dictionary.Add (key, valueFunc (content));
			}

			return dictionary;
		}

		#endregion


		#region Get Value

		public static TValue GetValue<TKey, TValue> (Dictionary<TKey, TValue> dict, TKey key)
		{
			TValue val;
			if (dict != null) {
				dict.TryGetValue (key, out val);
			} else {
				val = default(TValue);
			}
			return val;
		}

		public static TValue GetOrAddNewValue<TKey, TValue> (Dictionary<TKey, TValue> dict, TKey key) where TValue : class, new ()
		{
			if (dict == null) {
				return default(TValue);
			}

			if (dict.ContainsKey (key)) {
				return dict[key];
			}

			var val = new TValue ();
			dict.Add (key, val);
			return val;
		}

		public static TValue GetOrAddDefaultValue<TKey, TValue> (Dictionary<TKey, TValue> dict, TKey key)
		{
			if (dict == null) {
				return default(TValue);
			}

			if (dict.ContainsKey (key)) {
				return dict[key];
			}

			var val = default(TValue);
			dict.Add (key, val);
			return val;
		}

		#endregion


		#region For Each

		public static void ForEach<TKey, TValue> (Dictionary<TKey, TValue> dict, Action<TKey, TValue> action)
		{
			if (dict == null || action == null) {
				return;
			}

			foreach (var kvp in dict) {
				action (kvp.Key, kvp.Value);
			}
		}

		public static void ForEach<TKey, TValue> (this Dictionary<TKey, TValue> dict, Action<TValue> action)
		{
			if (dict == null || action == null) {
				return;
			}

			foreach (var kvp in dict) {
				action (kvp.Value);
			}
		}

		#endregion
	}
}