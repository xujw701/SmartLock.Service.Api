using Ninject.Modules;

namespace SmartELock.Service.Api.IoC
{
	public class RepositoryModule : NinjectModule
	{
		public override void Load()
		{
			//Bind<IUserRepository>().To<UserRepository>();
        }
	}
}
