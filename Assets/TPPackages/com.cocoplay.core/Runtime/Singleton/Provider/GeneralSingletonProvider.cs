namespace TC.Core.Singleton
{
	public class GeneralSingletonProvider<TSingleton> : ISingletonProvider<TSingleton> where TSingleton : new ()
	{
		public virtual TSingleton ProvideSingleton ()
		{
			return new TSingleton ();
		}
	}
}