using System.Collections.Generic;
using UnityEngine;

namespace TC.Core
{
	[System.Serializable]
	public class SerializableHashSet<T> : HashSet<T>, ISerializationCallbackReceiver
	{
		[SerializeField]
		private List<T> values = new List<T> ();

		public void OnBeforeSerialize ()
		{
			values.Clear ();
			values.Capacity = Count;
			foreach (var value in this) {
				values.Add (value);
			}
		}

		public void OnAfterDeserialize ()
		{
			Clear ();
			foreach (var value in values) {
				Add (value);
			}
		}
	}
}