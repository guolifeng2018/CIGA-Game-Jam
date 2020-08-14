using UnityEngine;

namespace TC.Core.Singleton
{
	public abstract class MonoDefaultSingleton<TSingleton> : MonoSingleton<TSingleton, ComponentSingletonProvider<TSingleton>>
		where TSingleton : MonoBehaviour
	{
	}
}