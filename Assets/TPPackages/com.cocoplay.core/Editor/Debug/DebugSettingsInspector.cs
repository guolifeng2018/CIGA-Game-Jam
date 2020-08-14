using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TC.Core.Editor
{
	[CustomEditor (typeof(DebugSettings))]
	public class DebugSettingsInspector : UnityEditor.Editor
	{
		public class Styles
		{
			public static readonly string ModuleSettingsPropertyName = "settings";

			public static readonly float DefaultIndentWidth = 12f;
			public static readonly float DefaultLineSpace = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

			private static GUIStyle boldLabelStyle = null; // = new GUIStyle (EditorStyles.label);

			public static readonly GUIContent Modules = new GUIContent ("Module Datas", "Module datas in debug settings");
			public static readonly GUIContent EditorOnlyModules = new GUIContent ("Module Datas (Editor Only)", "Module datas in debug settings (Editor Only)");

//			public Styles ()
//			{
//				BoldLabelStyle.fontStyle = FontStyle.Bold;
//			}

			public static GUIStyle BoldLabelStyle {
				get { return boldLabelStyle ?? (boldLabelStyle = new GUIStyle (EditorStyles.label) { fontStyle = FontStyle.Bold }); }
			}
		}

		private class ModuleListDrawer
		{
			private readonly SerializedProperty _modules;
			private readonly GUIContent _headerStyle;
			private readonly Type _moduleType;
			private readonly Func<string, string> _moduleShortNameFunc;

			private ReorderableList _modulesList;
			private EditorBoolPref[] _foldouts;
			private readonly List<SerializedObject> _moduleSos = new List<SerializedObject> ();

			public ModuleListDrawer (SerializedProperty modules, GUIContent headerStyle, Type moduleType, Func<string, string> moduleShortNameFunc)
			{
				_modules = modules;
				_headerStyle = headerStyle;
				_moduleType = moduleType;
				_moduleShortNameFunc = moduleShortNameFunc;
			}

			public void Draw ()
			{
				if (_modulesList == null) {
					OnValidate ();
				}

				_modulesList.DoLayoutList ();
			}

			private void OnValidate ()
			{
				CreateFoldoutPrefs ();

				_modulesList = new ReorderableList (_modules.serializedObject, _modules);
				_modulesList.drawElementCallback = DrawModule;
				_modulesList.elementHeightCallback = GetModuleHeight;
				_modulesList.drawHeaderCallback = DrawModuleHeader;

				_modulesList.onAddCallback += OnAddModule;
				_modulesList.onRemoveCallback = OnRemoveModule;
				_modulesList.onReorderCallbackWithDetails += OnReorderModule;
			}

			private void DrawModuleHeader (Rect rect)
			{
				EditorGUI.LabelField (rect, _headerStyle, GUIContent.none);
			}

			private void DrawModule (Rect rect, int index, bool isActive, bool isFocused)
			{
				var element = _modulesList.serializedProperty.GetArrayElementAtIndex (index);
				if (element == null) {
					return;
				}

				if (index % 2 != 0) {
					EditorGUI.DrawRect (new Rect (rect.x - 19f, rect.y, rect.width + 23f, rect.height), new Color (0, 0, 0, 0.1f));
				}

				EditorGUI.BeginChangeCheck ();

				var propRect = new Rect (rect.x, rect.y + EditorGUIUtility.standardVerticalSpacing,
					rect.width, EditorGUIUtility.singleLineHeight);

				if (element.objectReferenceValue != null) {
					var elementObj = element.objectReferenceValue;

					// header
					var headerRect = new Rect (rect.x + Styles.DefaultIndentWidth, rect.y + EditorGUIUtility.standardVerticalSpacing,
						rect.width - Styles.DefaultIndentWidth, EditorGUIUtility.singleLineHeight);
					var headerContent = new GUIContent (elementObj.name, elementObj.GetType ().Name);
					var foldout = _foldouts [index];
					foldout.Value = EditorGUI.Foldout (headerRect, foldout.Value, headerContent, true, Styles.BoldLabelStyle);
					if (foldout.Value) {
						EditorGUI.indentLevel++;

						// settings
						var elementSo = GetModuleSo (index);
						var settings = elementSo.FindProperty (Styles.ModuleSettingsPropertyName);
						if (settings != null) {
							propRect.y += Styles.DefaultLineSpace + EditorGUIUtility.standardVerticalSpacing;

							EditorGUI.BeginChangeCheck ();
							EditorGUI.PropertyField (propRect, settings, true);
							if (EditorGUI.EndChangeCheck ()) {
								elementSo.ApplyModifiedProperties ();
							}
						}

						EditorGUI.indentLevel--;
					}
				} else {
					EditorGUI.ObjectField (propRect, element, GUIContent.none);
				}

				if (EditorGUI.EndChangeCheck ()) {
					element.serializedObject.ApplyModifiedProperties ();
				}
			}

			private float GetModuleHeight (int index)
			{
				var height = Styles.DefaultLineSpace + EditorGUIUtility.standardVerticalSpacing;

				var element = _modulesList.serializedProperty.GetArrayElementAtIndex (index);
				if (element.objectReferenceValue == null || !_foldouts [index].Value) {
					return height;
				}

				height += EditorGUIUtility.standardVerticalSpacing;
				var elementSo = GetModuleSo (index);
				var settings = elementSo.FindProperty (Styles.ModuleSettingsPropertyName);
				if (settings != null) {
					height += EditorGUI.GetPropertyHeight (settings) + EditorGUIUtility.standardVerticalSpacing * 2;
				}

				return height;
			}

			private string GetModuleShortName (Type moduleType)
			{
				return _moduleShortNameFunc (moduleType.Name);
			}

			private void OnAddModule (ReorderableList list)
			{
				var menu = new GenericMenu ();

				var moduleTypes = AppDomain.CurrentDomain.GetAssemblies ().SelectMany (assembly => assembly.GetTypes ())
					.Where (type => type.IsClass && !type.IsAbstract && type.IsSubclassOf (_moduleType));
				foreach (var moduleType in moduleTypes) {
					var moduleName = GetModuleShortName (moduleType);
					moduleName = Regex.Replace (Regex.Replace (moduleName, "([a-z])([A-Z])", "$1 $2"),
						"([A-Z])([A-Z][a-z])", "$1 $2");
					menu.AddItem (new GUIContent (moduleName), false, AddModuleHandler, moduleType);
				}

				menu.ShowAsContext ();
			}

			private void AddModuleHandler (object moduleTypeObj)
			{
				_modules.serializedObject.ApplyModifiedProperties ();

				if (_modulesList.serializedProperty != null) {
					var asset = AssetDatabase.GetAssetOrScenePath (_modules.serializedObject.targetObject);
					var moduleType = (Type) moduleTypeObj;
					var module = CreateInstance (moduleType);
					module.name = GetModuleShortName (moduleType);
					AssetDatabase.AddObjectToAsset (module, asset);

					_modulesList.serializedProperty.arraySize++;
					_modulesList.index = _modulesList.serializedProperty.arraySize - 1;
					_modulesList.serializedProperty.serializedObject.ApplyModifiedProperties ();
					_modulesList.serializedProperty.GetArrayElementAtIndex (_modulesList.index).objectReferenceValue = module;
					_modulesList.serializedProperty.serializedObject.ApplyModifiedProperties ();
					AssetDatabase.SaveAssets ();
				}

				_moduleSos.Clear ();
				GetModuleSo (_modulesList.index);
				CreateFoldoutPrefs ();
				EditorUtility.SetDirty (_modules.serializedObject.targetObject);
			}

			private void OnRemoveModule (ReorderableList list)
			{
				var module = _modules.GetArrayElementAtIndex (list.index).objectReferenceValue;
				if (module != null) {
					var message = string.Format ("Are you sure you want to remove the module {0}, this operation cannot be undone", module.name);
					if (!EditorUtility.DisplayDialog ("Remove Module Settings", message, "Remove", "Cancel")) {
						return;
					}

					DestroyImmediate (module, true);
					AssetDatabase.SaveAssets ();
				}

				ReorderableList.defaultBehaviours.DoRemoveButton (list);
				if (list.index >= 0) {
					_modules.DeleteArrayElementAtIndex (list.index);
				}
				_modules.serializedObject.ApplyModifiedProperties ();
				_moduleSos.Clear ();
			}

			private void OnReorderModule (ReorderableList list, int oldIndex, int newIndex)
			{
				var tempSo = _moduleSos [oldIndex];
				_moduleSos.RemoveAt (oldIndex);
				_moduleSos.Insert (newIndex, tempSo);

				var tempFoldout = _foldouts [oldIndex].Value;
				_foldouts [oldIndex].Value = _foldouts [newIndex].Value;
				_foldouts [newIndex].Value = tempFoldout;
			}

			private void CreateFoldoutPrefs ()
			{
				_foldouts = new EditorBoolPref[_modules.arraySize];
				for (var i = 0; i < _modules.arraySize; i++) {
					var modulesName = _modules.serializedObject.targetObject.name;
					var foldoutName = string.Format ("{0}.ELEMENT{1}.Foldout", modulesName, i);
					_foldouts [i] = new EditorBoolPref (foldoutName, false);
				}
			}

			private SerializedObject GetModuleSo (int index)
			{
				if (_moduleSos.Count != _modules.arraySize || _moduleSos [index] == null) {
					_moduleSos.Clear ();
					for (var i = 0; i < _modules.arraySize; i++) {
						var element = _modules.GetArrayElementAtIndex (i);
						if (element == null) {
							continue;
						}

						var elementObj = element.objectReferenceValue;
						_moduleSos.Add (elementObj != null ? new SerializedObject (elementObj) : null);
					}
				}

				var moduleSo = _moduleSos [index];
				moduleSo.Update ();
				return moduleSo;
			}
		}

		private SerializedProperty _enabled;

		private ModuleListDrawer _moduleListDrawer;
		private ModuleListDrawer _editorOnlyModuleListDrawer;

		public override void OnInspectorGUI ()
		{
			serializedObject.Update ();

			if (!IsValidated) {
				OnValidate ();
			}

			EditorGUILayout.PropertyField (_enabled);

			EditorGUILayout.Separator ();
			_moduleListDrawer.Draw ();

			EditorGUILayout.Separator ();
			_editorOnlyModuleListDrawer.Draw ();

			serializedObject.ApplyModifiedProperties ();
		}

		private bool IsValidated {
			get { return _enabled != null && _moduleListDrawer != null && _editorOnlyModuleListDrawer != null; }
		}

		private void OnValidate ()
		{
			_enabled = serializedObject.FindProperty ("isEnabled");

			var modules = serializedObject.FindProperty ("moduleDatas");
			_moduleListDrawer = new ModuleListDrawer (modules, Styles.Modules, typeof(ModuleDebugData),
				moduleName => moduleName.Replace ("DebugData", ""));

			var editorOnlyModules = serializedObject.FindProperty ("editorOnlyDatas");
			_editorOnlyModuleListDrawer = new ModuleListDrawer (editorOnlyModules, Styles.EditorOnlyModules, typeof(EditorOnlyDebugData),
				moduleName => moduleName.Replace ("EditorOnlyDebugData", " (EditorOnly)"));
		}
	}
}