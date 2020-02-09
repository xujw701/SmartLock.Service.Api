using Ninject.Modules;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Repositories.Repositories;

namespace SmartELock.Service.Api.IoC
{
	public class RepositoryModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ISuperAdminRepository>().To<SuperAdminRepository>();
			Bind<ICompanyRepository>().To<CompanyRepository>();
			Bind<IBranchRepository>().To<BranchRepository>();
			Bind<IUserRepository>().To<UserRepository>();
			Bind<IKeyboxAssetRepository>().To<KeyboxAssetRepository>();
			Bind<IKeyboxRepository>().To<KeyboxRepository>();
			Bind<IPropertyRepository>().To<PropertyRepository>();
		}
	}
}
