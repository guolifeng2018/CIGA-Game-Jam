using UnityEditor;
using UnityEngine;

namespace TC.Core.Editor
{
	[CustomPropertyDrawer (typeof(OptionalValueBase), true)]
	public class OptionalValueDrawer : PropertyDrawer
	{
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty (position, label, property);
			var contentRect = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

			var enabled = EnabledProperty (property);

			var rect = new Rect (contentRect.x, contentRect.y, 15, contentRect.height);
			EditorGUI.PropertyField (rect, enabled, GUIContent.none);

			rect.x += rect.width + 10;
			rect.width = contentRect.xMax - rect.x;
			if (enabled.boolValue) {
				DrawValue (position, rect, property);
			} else {
				var guiEnabled = GUI.enabled;
				GUI.enabled = false;
				EditorGUI.LabelField (rect, "Unused");
				GUI.enabled = guiEnabled;
			}

			EditorGUI.EndProperty ();
		}

		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			var height = base.GetPropertyHeight (property, label);
			var enabled = EnabledProperty (property);
			if (enabled.boolValue) {
				height += ValueExpandedHeight;
			}
			return height;
		}

		protected SerializedProperty EnabledProperty (SerializedProperty property)
		{
			return property.FindPropertyRelative ("inUse");
		}

		protected SerializedProperty ValueProperty (SerializedProperty property)
		{
			return property.FindPropertyRelative ("value");
		}

		protected virtual void DrawValue (Rect position, Rect valueRect, SerializedProperty property)
		{
			EditorGUI.PropertyField (valueRect, ValueProperty (property), GUIContent.none, true);
		}

		protected virtual float ValueExpandedHeight {
			get { return 0; }
		}
	}
}