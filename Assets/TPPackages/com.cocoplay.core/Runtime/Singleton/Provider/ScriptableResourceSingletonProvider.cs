using UnityEngine;

namespace TC.Core.Singleton
{
	public class ScriptableResourceSingletonProvider<TSingleton> : ScriptableSingletonProvider<TSingleton>, IResourceSingletonProvider
		where TSingleton : ScriptableObject
	{
		public override TSingleton ProvideSingleton ()
		{
			var instance = Resources.Load<TSingleton> (SingletonResourcePath);
			if (instance != null) {
				return instance;
			}

			Debug.LogWarningFormat ("{0}->ProvideSingleton: can NOT load from resource path {1}, provide default.", GetType ().Name, SingletonResourcePath);
			return base.ProvideSingleton ();
		}


		public virtual string SingletonResourcePath {
			get { return SingletonUtil.DefaultResourcePath<TSingleton> (); }
		}
	}
}