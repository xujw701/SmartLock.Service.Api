using SmartELock.Service.Api.ExceptionHandlers;
using SmartELock.Service.Api.Http;

namespace SmartELock.Service.Api
{
	public class ErrorHandlerConfig
	{
		/// <summary>
		/// Registers the error handlers.
		/// </summary>
		public static void RegisterErrorHandlers()
		{
			UnhandledWebExceptionManager.AddHandler(new DomainValidationExceptionHandler());
		}
	}
}