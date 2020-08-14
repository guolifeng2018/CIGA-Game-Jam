using UnityEngine;

namespace TC.Core.Singleton
{
	public static class SingletonUtil
	{
		#region Instance

		public static TSingleton Create<TSingleton, TProvider> () where TProvider : ISingletonProvider<TSingleton>, new ()
		{
			var singleton = Get<TSingleton> ();
			if (singleton != null) {
				return singleton;
			}

			// create
			singleton = new TProvider ().ProvideSingleton ();
			if (singleton == null) {
				Debug.LogErrorFormat ("SingletonUtil->Create: can NOT create singleton [{0}] by provider [{1}]",
					typeof(TSingleton).Name, typeof(TProvider).Name);
				return default(TSingleton);
			}

			Bind (singleton);
			CallOnCreateIfPossible (singleton);

			return singleton;
		}

		public static TSingleton Get<TSingleton> ()
		{
			return InstanceContainer.Get<TSingleton> ();
		}

		public static void Bind (object singleton)
		{
			InstanceContainer.Bind (singleton);
		}

		public static void Unbind (object singleton)
		{
			InstanceContainer.UnbindAll (singleton);
		}

		#endregion


		#region Callback

		public static void CallOnCreateIfPossible (object singleton)
		{
			var handler = singleton as ISingletonCreateHandler;
			if (handler != null) {
				handler.OnSingletonCreated ();
			}
		}

		public static void CallOnAwakeIfPossible (object singleton)
		{
			var handler = singleton as IMonoSingletonAwakeHandler;
			if (handler != null) {
				handler.OnSingletonAwake ();
			}
		}

		public static void CallOnDestroyIfPossible (object singleton)
		{
			var createHandler = singleton as IMonoSingletonDestroyHandler;
			if (createHandler != null) {
				createHandler.OnSingletonDestroy ();
			}
		}

		#endregion


		#region Helper

		public static string DefaultResourcePath<TSingleton> ()
		{
			return string.Format ("singleton/{0}", typeof(TSingleton).Name);
		}

		#endregion
	}
}