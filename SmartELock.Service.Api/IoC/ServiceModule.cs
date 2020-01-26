using Ninject.Modules;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Services;
using SmartELock.Core.Services.Services;
using SmartELock.Core.Services.Validators;

namespace SmartELock.Service.Api.IoC
{
	public class ServiceModule : NinjectModule
	{
		public override void Load()
		{
            Bind<ISuperAdminService>().To<SuperAdminService>();
			Bind<ICompanyService>().To<CompanyService>();
			Bind<IBranchService>().To<BranchService>();
			Bind<IUserService>().To<UserService>();

			Bind<ICommandValidator<SuperAdminCreateCommand>>().To<SuperAdminCreateValidator>();
			Bind<ICommandValidator<CompanyCreateCommand>>().To<CompanyCreateValidator>();
		}
	}
}
