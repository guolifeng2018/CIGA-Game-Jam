using UnityEngine;

namespace TC.Core.Singleton
{
	public class ComponentSingletonProvider<TSingleton> : ISingletonProvider<TSingleton> where TSingleton : Component
	{
		public virtual TSingleton ProvideSingleton ()
		{
			var instanceGo = new GameObject ("_Singleton_" + typeof(TSingleton).Name);
			return instanceGo.AddComponent<TSingleton> ();
		}
	}
}