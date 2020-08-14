namespace TC.Core.Singleton
{
	public abstract class GeneralDefaultSingleton<TSingleton> : GeneralSingleton<TSingleton, GeneralSingletonProvider<TSingleton>>
		where TSingleton : GeneralDefaultSingleton<TSingleton>, new ()
	{
	}
}