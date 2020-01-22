using Ninject.Modules;
using SmartELock.Service.Api.Mapper;

namespace SmartELock.Service.Api.IoC
{
	public class MapperModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ISuperAdminMapper>().To<SuperAdminMapper>();
			Bind<ICompanyMapper>().To<CompanyMapper>();
		}
	}
}