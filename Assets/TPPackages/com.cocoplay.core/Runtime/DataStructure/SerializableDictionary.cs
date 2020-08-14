using System.Collections.Generic;
using UnityEngine;

namespace TC.Core
{
	[System.Serializable]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField]
		private List<TKey> keys = new List<TKey> ();

		[SerializeField]
		private List<TValue> values = new List<TValue> ();

		public void OnBeforeSerialize ()
		{
			keys.Clear ();
			values.Clear ();
			keys.Capacity = Count;
			values.Capacity = Count;
			foreach (var kvp in this) {
				keys.Add (kvp.Key);
				values.Add (kvp.Value);
			}
		}

		public void OnAfterDeserialize ()
		{
			Clear ();
			var count = Mathf.Min (keys.Count, values.Count);
			for (var i = 0; i < count; ++i) {
				Add (keys[i], values[i]);
			}
		}
	}
}