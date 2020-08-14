using UnityEngine;

namespace TC.Core.Singleton
{
	public class ResourceSingletonProvider<TSingleton> : ISingletonProvider<TSingleton>, IResourceSingletonProvider where TSingleton : Object
	{
		public TSingleton ProvideSingleton ()
		{
			return Resources.Load<TSingleton> (SingletonResourcePath);
		}

		public virtual string SingletonResourcePath {
			get { return SingletonUtil.DefaultResourcePath<TSingleton> ();; }
		}
	}
}