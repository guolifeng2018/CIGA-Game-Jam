using UnityEngine;

namespace TC.Core.Singleton
{
	public abstract class ScriptableDefaultSingleton<TSingleton> : ScriptableSingleton<TSingleton, ScriptableSingletonProvider<TSingleton>>
		where TSingleton : ScriptableObject
	{
	}
}