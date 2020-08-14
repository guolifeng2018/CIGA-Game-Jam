using UnityEngine;

namespace TC.Core.Singleton
{
	public abstract class ScriptableResourceDefaultSingleton<TSingleton> : ScriptableSingleton<TSingleton, ScriptableResourceSingletonProvider<TSingleton>>
		where TSingleton : ScriptableObject
	{
	}
}