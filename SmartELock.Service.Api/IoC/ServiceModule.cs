using Ninject.Modules;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Commands.Base;
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

			Bind<IOtherService>().To<OtherService>();

			Bind<IPushNotificationService>().To<PushNotificationService>();

			Bind<ICommandValidator<SuperAdminCreateCommand>>().To<SuperAdminCreateValidator>();
			Bind<ICommandValidator<CompanyCreateCommand>>().To<CompanyCreateValidator>();
			Bind<ICommandValidator<BranchCreateCommand>>().To<BranchCreateValidator>();
			Bind<ICommandValidator<BranchUpdateCommand>>().To<BranchUpdateValidator>();
			Bind<ICommandValidator<UserCreateCommand>>().To<UserCreateValidator>();
			Bind<ICommandValidator<KeyboxAssetCreateCommand>>().To<KeyboxAssetCreateValidator>();
			Bind<ICommandValidator<KeyboxCreateCommand>>().To<KeyboxRegisterValidator>();
			Bind<ICommandValidator<KeyboxUpdateCommand>>().To<KeyboxUpdateValidator>();
			Bind<ICommandValidator<KeyboxAssignToCommand>>().To<KeyboxAssignToValidator>();
			Bind<ICommandValidator<KeyboxPropertyCreateCommand>>().To<KeyboxPropertyCreateValidator>();
			Bind<ICommandValidator<KeyboxPropertyUpdateCommand>>().To<KeyboxPropertyUpdateValidator>();
			Bind<ICommandValidator<KeyboxPropertyCommand>>().To<KeyboxPropertyOperateValidator>();
			Bind<ICommandValidator<KeyboxCommand>>().To<KeyboxAccessValidator>();
		}
	}
}
