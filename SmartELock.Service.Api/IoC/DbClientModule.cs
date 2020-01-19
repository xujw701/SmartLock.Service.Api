using SmartELock.Core.Repositories.Infrastructure;
using Ninject.Modules;

namespace SmartELock.Service.Api.IoC
{
	public class DbClientModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IConnectionFactory>().To<ConnectionFactory>();
			Bind<IDbRetryHandler>().To<DbRetryHandler>();
		}
	}
}