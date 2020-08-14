using UnityEditor;

namespace TC.Core.Editor
{
	internal class EditorBoolPref
	{
		private readonly string _name;
		private bool _value;

		public EditorBoolPref (string name, bool defaultValue)
		{
			_name = name;
			_value = EditorPrefs.GetBool (_name, defaultValue);
		}

		public bool Value {
			get { return _value; }
			set {
				if (_value == value) {
					return;
				}

				_value = value;
				EditorPrefs.SetBool (_name, value);
			}
		}
	}
}