using Ninject.Modules;
using SmartELock.Core.Domain.Repository;
using SmartELock.Core.Repositories;

namespace SmartELock.Service.Api.IoC
{
	public class RepositoryModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ISuperAdminRepository>().To<SuperAdminRepository>();
			Bind<ICompanyRepository>().To<CompanyRepository>();
		}
	}
}
