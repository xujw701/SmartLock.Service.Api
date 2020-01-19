using System.Configuration;
using System.Web.Http;
using SmartELock.Service.Api.IoC;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(SmartELock.Service.Api.Startup))]

namespace SmartELock.Service.Api
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			IKernel kernel = CreateKernel();
			var config = new HttpConfiguration
			{
				IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
			};

			//ConfigureOAuth(app);

			WebApiConfig.Register(config);

			app.UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);
		}

		private static StandardKernel CreateKernel()
		{
			var kernel = new StandardKernel();

			kernel.Load(new DbClientModule());
			kernel.Load(new RepositoryModule());
			kernel.Load(new ServiceModule());
			kernel.Load(new MapperModule());

			return kernel;
		}
	}
}
