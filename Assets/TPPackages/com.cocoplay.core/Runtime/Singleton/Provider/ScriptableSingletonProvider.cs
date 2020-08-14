using UnityEngine;

namespace TC.Core.Singleton
{
	public class ScriptableSingletonProvider<TSingleton> : ISingletonProvider<TSingleton> where TSingleton : ScriptableObject
	{
		public virtual TSingleton ProvideSingleton ()
		{
			return ScriptableObject.CreateInstance<TSingleton> ();
		}
	}
}