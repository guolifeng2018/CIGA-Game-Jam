using UnityEngine;

namespace TC.Core.Singleton
{
	public class ResourceInstantiateSingletonProvider<TSingleton> : ISingletonProvider<TSingleton>, IResourceSingletonProvider where TSingleton : Object
	{
		public TSingleton ProvideSingleton ()
		{
			var original = Resources.Load<TSingleton> (SingletonResourcePath);
			return original != null ? Object.Instantiate (original) : null;
		}

		public virtual string SingletonResourcePath {
			get { return SingletonUtil.DefaultResourcePath<TSingleton> (); }
		}
	}
}