namespace TC.Core.Singleton
{
	public abstract class GeneralSingleton<TSingleton, TProvider>
		where TProvider : ISingletonProvider<TSingleton>, new ()
	{
		#region Instance

		private static TSingleton instance;

		public static TSingleton Instance {
			get {
				if (instance == null) {
					instance = SingletonUtil.Create<TSingleton, TProvider> ();
				}
				return instance;
			}
		}

		#endregion
	}
}