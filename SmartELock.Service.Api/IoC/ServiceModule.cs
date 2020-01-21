using Ninject.Modules;
using SmartELock.Core.Domain.Service;
using SmartELock.Core.Services;

namespace SmartELock.Service.Api.IoC
{
	public class ServiceModule : NinjectModule
	{
		public override void Load()
		{
            Bind<ISuperAdminService>().To<SuperAdminService>();

            //Bind<ICommandValidator<CompanyCreateCommand>>().To<CompanyCreateValidator>();
        }
	}
}
