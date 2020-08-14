using System;
using UnityEditor;
using UnityEngine;

namespace TC.Core.Editor
{
	[CustomPropertyDrawer (typeof(EditorOnlyDebugData.SettingsBase), true)]
	public class EditorOnlyDebugSettingsDrawer : PropertyDrawer
	{
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			var rect = new Rect (position.x, position.y, position.width, 0);
			ForeachChildProperties (property, childProperty => {
				rect.y += rect.height;
				rect.height = EditorGUI.GetPropertyHeight (childProperty);
				EditorGUI.PropertyField (rect, childProperty, new GUIContent (childProperty.displayName), true);
			});
		}

		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			var height = 0f;
			ForeachChildProperties (property, childProperty => height += EditorGUI.GetPropertyHeight (childProperty));
			return height;
		}

		private void ForeachChildProperties (SerializedProperty property, Action<SerializedProperty> childAction)
		{
			if (childAction == null) {
				return;
			}

			var propertyIt = property.Copy ();
			if (!propertyIt.NextVisible (true)) {
				// no child or invisible
				return;
			}

			do {
				childAction (propertyIt);
			} while (propertyIt.NextVisible (false));
		}
	}
}