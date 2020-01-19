using Ninject.Modules;

namespace SmartELock.Service.Api.IoC
{
	public class ServiceModule : NinjectModule
	{
		public override void Load()
		{
            //Bind<IUserService>().To<UserService>();

            //Bind<ICommandValidator<CompanyCreateCommand>>().To<CompanyCreateValidator>();
        }
	}
}
