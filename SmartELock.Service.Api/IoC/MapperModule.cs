using Ninject.Modules;

namespace SmartELock.Service.Api.IoC
{
	public class MapperModule : NinjectModule
	{
		public override void Load()
		{
			//Bind<IUserMapper>().To<UserMapper>();
        }
	}
}