using UnityEngine;

namespace TC.Core.Singleton
{
	public abstract class MonoResourceDefaultSingleton<TSingleton> : MonoSingleton<TSingleton, ResourceComponentSingletonProvider<TSingleton>>
		where TSingleton : MonoBehaviour
	{
	}
}