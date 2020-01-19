using System;
using System.Web.Http.Filters;

namespace SmartELock.Service.Api.Http
{
	public interface IWebExceptionHandler
	{
		/// <summary>
		/// Handles the exception.
		/// </summary>
		/// <param name="actionExecutedContext">The action executed context.</param>
		/// <param name="exception">The exception instance.</param>
		void HandleException(HttpActionExecutedContext actionExecutedContext, Exception exception);
	}
}