using System.Collections.Generic;
using TC.Core.Singleton;
using UnityEngine;

namespace TC.Core
{
	public class DebugSettingsSingletonProvider : ScriptableResourceSingletonProvider<DebugSettings>
	{
		public override string SingletonResourcePath {
			get { return DebugSettings.RESOURCE_PATH; }
		}
	}

	public class DebugSettings : ScriptableSingleton<DebugSettings, DebugSettingsSingletonProvider>
	{
		public const string RESOURCE_PATH = "debug/debug_settings";

		[SerializeField]
		private bool isEnabled;

		public bool IsEnabled {
			get { return isEnabled; }
			set { isEnabled = value; }
		}


		#region Module Data

		[SerializeField]
		private List<ModuleDebugData> moduleDatas = new List<ModuleDebugData> ();

		public List<ModuleDebugData> ModuleDatas {
			get { return moduleDatas; }
		}

		private Dictionary<string, ModuleDebugData> _moduleDataDic = null;

		private Dictionary<string, ModuleDebugData> ModuleDataDic {
			get { return _moduleDataDic ?? (_moduleDataDic = DictionaryUtil.Create (moduleDatas, setting => setting.GetType ().Name)); }
		}

		public T GetModuleData<T> () where T : ModuleDebugData
		{
			var typeName = typeof(T).Name;
			var moduleSetting = ModuleDataDic.GetValue (typeName);
			return moduleSetting as T;
		}

		#endregion


		#region Editor Data

		[SerializeField]
		private List<EditorOnlyDebugData> editorOnlyDatas = new List<EditorOnlyDebugData> ();

		public List<EditorOnlyDebugData> EditorOnlyDatas {
			get { return editorOnlyDatas; }
		}

		private Dictionary<string, EditorOnlyDebugData> _editorDataDic = null;

		private Dictionary<string, EditorOnlyDebugData> EditorDataDic {
			get { return _editorDataDic ?? (_editorDataDic = DictionaryUtil.Create (editorOnlyDatas, setting => setting.GetType ().Name)); }
		}

		public T GetEditorData<T> () where T : EditorOnlyDebugData
		{
			var typeName = typeof(T).Name;
			var editorSetting = EditorDataDic.GetValue (typeName);
			return editorSetting as T;
		}

		#endregion
	}
}