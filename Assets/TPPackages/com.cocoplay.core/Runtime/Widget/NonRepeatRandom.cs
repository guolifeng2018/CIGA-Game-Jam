using UnityEngine;

namespace TC.Core
{
	public class NonRepeatRandom
	{
		private readonly int[] _ids;
		private readonly int _count;

		private int _firstIndex;
		private int _lastIndex;
		private bool _backward;

		public NonRepeatRandom (int count)
		{
			_count = count;
			if (_count <= 0) {
				return;
			}

			_ids = new int[_count];
			for (var i = 0; i < _count; i++) {
				_ids [i] = i;
			}

			Reset ();
		}

		public int Count {
			get { return _count; }
		}

		public int RandomId {
			get {
				if (_count <= 0) {
					return -1;
				}
				if (_count == 1) {
					return _ids [0];
				}

				var index = Random.Range (_firstIndex, _lastIndex);
				var id = _ids [index];

//				Debug.LogWarning (GetHashCode () + ": (" + _firstIndex + ", " + _lastIndex + ") -> [" + index + "]" + id + ", " + _backward);

				if (_backward) {
					if (_firstIndex < _count - 1) {
						_ids [index] = _ids [_firstIndex];
						_ids [_firstIndex] = id;
						_firstIndex++;
					} else {
						_backward = false;
						_firstIndex = 0;
						_lastIndex = _count - 1;
					}
				} else {
					_lastIndex--;
					if (_lastIndex > 0) {
						_ids [index] = _ids [_lastIndex];
						_ids [_lastIndex] = id;
					} else {
						_backward = true;
						_firstIndex = 1;
						_lastIndex = _count;
					}
				}

//				var str = GetHashCode () + ": [";
//				for (var i = 0; i < _count; i++) {
//					str += _ids [i] + ", ";
//				}
//				Debug.LogWarning (str + "]");

				return id;
			}
		}

		public void Reset ()
		{
			_firstIndex = 0;
			_lastIndex = _count;
			_backward = false;
		}
	}
}