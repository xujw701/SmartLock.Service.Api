using Ninject.Modules;
using SmartELock.Service.Api.Mappers;

namespace SmartELock.Service.Api.IoC
{
	public class MapperModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ISuperAdminMapper>().To<SuperAdminMapper>();
			Bind<ICompanyMapper>().To<CompanyMapper>();
			Bind<IBranchMapper>().To<BranchMapper>();
			Bind<IUserMapper>().To<UserMapper>();
			Bind<IKeyboxMapper>().To<KeyboxMapper>();
		}
	}
}