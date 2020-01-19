using System.Linq;
using System.Web.Http;
using SmartELock.Service.Api.Filters;
using SmartELock.Service.Api.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SmartELock.Service.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Web API configuration and services
			// Configure Web API to use only bearer token authentication.
			config.SuppressDefaultHostAuthentication();
			config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

			// Web API routes
			config.MapHttpAttributeRoutes();

			// Web API configuration and services
			config.Filters.Add(new ValidateModelAttribute());
			config.Filters.Add(new CustomErrorHandlerAttribute());

			config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			SetFormatters(config);

			ErrorHandlerConfig.RegisterErrorHandlers();
		}

		private static void SetFormatters(HttpConfiguration config)
		{
			// Remove xml formatting as default webapi response
			config.Formatters.Remove(config.Formatters.XmlFormatter);

			var jsonFormatter = config.Formatters.JsonFormatter;
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			jsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
			jsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
			jsonFormatter.SerializerSettings.Converters =
				jsonFormatter.SerializerSettings.Converters.Where(c => !(c is StringEnumConverter)).ToList();
			
			jsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter
			{
				CamelCaseText = false,
				AllowIntegerValues = false
			});
		}
	}
}
