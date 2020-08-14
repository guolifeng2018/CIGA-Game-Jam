using TC.Core.Singleton;
using UnityEngine;

namespace TC.Core
{
	public abstract class ModuleDebugData : ScriptableObject
	{
		public abstract class SettingsBase
		{
		}
	}

	public class ModuleDebugDataProvider<TData> : ISingletonProvider<TData> where TData : ModuleDebugData
	{
		public TData ProvideSingleton ()
		{
			return DebugSettings.Instance.GetModuleData<TData> ();
		}
	}

	public abstract class ModuleDebugData<TData, TSettings> : ModuleDebugData
		where TData : ModuleDebugData<TData, TSettings>
		where TSettings : ModuleDebugData.SettingsBase, new ()
	{
		[SerializeField]
		protected TSettings settings = new TSettings ();


		#region Instance

		private static TSettings settingsInstance = null;

		public static TSettings SettingsInstance {
			get {
				if (settingsInstance == null) {
					var instance = SingletonUtil.Create<TData, ModuleDebugDataProvider<TData>> ();
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