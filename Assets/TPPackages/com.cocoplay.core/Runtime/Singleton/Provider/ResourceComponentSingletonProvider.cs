using UnityEngine;

namespace TC.Core.Singleton
{
	public class ResourceComponentSingletonProvider<TSingleton> : ISingletonProvider<TSingleton>, IResourceSingletonProvider
		where TSingleton : Component
	{
		public TSingleton ProvideSingleton ()
		{
			return ObjectUtil.Instantiate<TSingleton> (SingletonResourcePath);
		}


		public virtual string SingletonResourcePath {
			get { return SingletonUtil.DefaultResourcePath<TSingleton> (); }
		}
	}
}