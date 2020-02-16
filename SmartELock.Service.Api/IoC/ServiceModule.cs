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
			Bind<IKeyboxService>().To<KeyboxService>();

			Bind<IAuthorizationService>().To<AuthorizationService>();

			Bind<ICommandValidator<SuperAdminCreateCommand>>().To<SuperAdminCreateValidator>();
			Bind<ICommandValidator<CompanyCreateCommand>>().To<CompanyCreateValidator>();
			Bind<ICommandValidator<BranchCreateCommand>>().To<BranchCreateValidator>();
			Bind<ICommandValidator<UserCreateCommand>>().To<UserCreateValidator>();
			Bind<ICommandValidator<KeyboxAssetCreateCommand>>().To<KeyboxAssetCreateValidator>();
			Bind<ICommandValidator<KeyboxCreateCommand>>().To<KeyboxRegisterValidator>();
			Bind<ICommandValidator<KeyboxAssignToCommand>>().To<KeyboxAssignToValidator>();
			Bind<ICommandValidator<KeyboxPropertyCreateCommand>>().To<KeyboxPropertyCreateValidator>();
			Bind<ICommandValidator<KeyboxPropertyUpdateCommand>>().To<KeyboxPropertyUpdateValidator>();
			Bind<ICommandValidator<KeyboxPropertyDeleteCommand>>().To<KeyboxPropertyDeleteValidator>();
		}
	}
}
