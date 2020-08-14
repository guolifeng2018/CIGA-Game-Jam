using UnityEngine;

namespace TC.Core.Singleton
{
	public abstract class ScriptableSingleton<TSingleton, TProvider> : ScriptableObject
		where TSingleton : ScriptableObject
		where TProvider : ISingletonProvider<TSingleton>, new ()
	{
		#region Instance

		private static TSingleton instance;

		public static TSingleton Instance {
			get {
				if (instance == null) {
					instance = SingletonUtil.Create<TSingleton, TProvider> ();
				}
				return instance;
			}
		}

		#endregion
	}
}