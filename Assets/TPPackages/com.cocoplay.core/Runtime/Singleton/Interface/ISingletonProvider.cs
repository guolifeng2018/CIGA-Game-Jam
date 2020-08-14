namespace TC.Core.Singleton
{
	public interface ISingletonProvider<TSingleton>
	{
		TSingleton ProvideSingleton ();
	}
}