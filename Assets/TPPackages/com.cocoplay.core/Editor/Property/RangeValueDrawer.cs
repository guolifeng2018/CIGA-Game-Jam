using UnityEditor;
using UnityEngine;

namespace TC.Core.Editor
{
	[CustomPropertyDrawer (typeof(RangeValueBase), true)]
	public class RangeValueDrawer : PropertyDrawer
	{
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty (position, label, property);
			var valueRect = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

			var originalIndentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			// from / to
			var valueWidth = valueRect.width * 2 / 5;
			var rect = new Rect (valueRect.x, valueRect.y, valueWidth, EditorGUIUtility.singleLineHeight);
			DrawSubValue (rect, property.FindPropertyRelative ("from"));
			rect.x = valueRect.xMax - valueWidth;
			DrawSubValue (rect, property.FindPropertyRelative ("to"));

			// <->
			rect.width = 26;
			rect.height = EditorGUIUtility.singleLineHeight;
			rect.x = valueRect.center.x - rect.width / 2;
			rect.y = valueRect.center.y - EditorGUIUtility.singleLineHeight / 2;
			EditorGUI.LabelField (rect, "<->");

			EditorGUI.indentLevel = originalIndentLevel;

			EditorGUI.EndProperty ();
		}

		protected virtual void DrawSubValue (Rect rect, SerializedProperty value)
		{
			EditorGUI.PropertyField (rect, value, GUIContent.none);
		}
	}

	[CustomPropertyDrawer (typeof(RangeVector2))]
	public class RangeVector2Drawer : RangeValueDrawer
	{
		protected override void DrawSubValue (Rect rect, SerializedProperty value)
		{
			var originLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 15;

			EditorGUI.PropertyField (rect, value.FindPropertyRelative ("x"));
			rect.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField (rect, value.FindPropertyRelative ("y"));

			EditorGUIUtility.labelWidth = originLabelWidth;
		}

		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight * 2;
		}
	}

	[CustomPropertyDrawer (typeof(RangeVector3))]
	public class RangeVector3Drawer : RangeValueDrawer
	{
		protected override void DrawSubValue (Rect rect, SerializedProperty value)
		{
			var originLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 15;

			EditorGUI.PropertyField (rect, value.FindPropertyRelative ("x"));
			rect.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField (rect, value.FindPropertyRelative ("y"));
			rect.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField (rect, value.FindPropertyRelative ("z"));

			EditorGUIUtility.labelWidth = originLabelWidth;
		}

		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight * 3;
		}
	}
}