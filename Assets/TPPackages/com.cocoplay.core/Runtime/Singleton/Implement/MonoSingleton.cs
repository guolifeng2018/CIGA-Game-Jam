using UnityEngine;

namespace TC.Core.Singleton
{
	public abstract class MonoSingleton<TSingleton, TProvider> : MonoBehaviour
		where TSingleton : MonoBehaviour
		where TProvider : ISingletonProvider<TSingleton>, new ()
	{
		#region Event Function

		protected void Awake ()
		{
			if (instance == null) {
				instance = SingletonUtil.Get<TSingleton> ();
			}
			if (instance != null) {
				if (this != instance) {
					DestroyDuplicate ();
					return;
				}
			} else {
				instance = this as TSingleton;
				if (!isCreatedByProvider) {
					SingletonUtil.Bind (instance);
					SingletonUtil.CallOnCreateIfPossible (instance);
				}
			}

			isOnDestroying = false;
			if (IsGlobal) {
				DontDestroyOnLoad (this);
			}

			SingletonUtil.CallOnAwakeIfPossible (instance);
		}

		protected void OnDestroy ()
		{
			if (this != instance) {
				return;
			}

			SingletonUtil.CallOnDestroyIfPossible (instance);

			SingletonUtil.Unbind (instance);
			instance = null;
			isOnDestroying = true;
			isCreatedByProvider = false;
		}

		protected virtual void OnApplicationQuit ()
		{
			isOnDestroying = true;
		}

		#endregion


		#region Instance

		private static bool isOnDestroying;

		public static bool IsOnDestroying {
			get { return isOnDestroying; }
		}

		private static bool isCreatedByProvider;

		public static bool IsCreatedByProvider {
			get { return isCreatedByProvider; }
		}

		private static TSingleton instance;

		public static TSingleton Instance {
			get {
				if (isOnDestroying) {
					return null;
				}

				if (instance == null) {
					isCreatedByProvider = true;
					instance = SingletonUtil.Create<TSingleton, TProvider> ();
				}
				return instance;
			}
		}

		#endregion


		#region Life Cycle

		protected virtual bool IsGlobal {
			get { return true; }
		}

		protected virtual bool DestroyGameObjectIfDuplicate {
			get { return true; }
		}

		private void DestroyDuplicate ()
		{
			Debug.LogWarningFormat ("{0}->DestroyDuplicate: destroy self [{1}] because instance [{2}] ALREADY exists.", GetType ().Name, name, instance.name);
			if (DestroyGameObjectIfDuplicate) {
				Destroy (gameObject);
			} else {
				Destroy (this);
			}
		}

		#endregion
	}
}