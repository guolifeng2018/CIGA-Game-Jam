using TC.Core.Singleton;
using UnityEngine;

namespace TC.Core
{
	public abstract class EditorOnlyDebugData : ScriptableObject
	{
		public abstract class SettingsBase
		{
		}
	}

	public class EditorOnlyDebugDataProvider<TData> : ISingletonProvider<TData> where TData : EditorOnlyDebugData
	{
		public TData ProvideSingleton ()
		{
			return DebugSettings.Instance.GetEditorData<TData> ();
		}
	}

	public abstract class EditorOnlyDebugData<TData, TSettings> : EditorOnlyDebugData
		where TData : EditorOnlyDebugData<TData, TSettings>
		where TSettings : EditorOnlyDebugData.SettingsBase, new ()
	{
		[SerializeField]
		protected TSettings settings = new TSettings ();


		#region Instance

		private static TSettings settingsInstance = null;

		public static TSettings SettingsInstance {
			get {
				if (settingsInstance == null) {
					var instance = SingletonUtil.Create<TData, EditorOnlyDebugDataProvider<TData>> ();
					if (instance != null) {
						settingsInstance = instance.settings;
					}
				}
				return settingsInstance;
			}
		}

		#endregion
	}
}