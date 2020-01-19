using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using SmartELock.Core.Domain.Models.Exceptions;

namespace SmartELock.Service.Api.Http
{
	public abstract class WebExceptionHandler<TException> : IWebExceptionHandler
	   where TException : Exception
	{

		/// <summary>
		///     Handles the exception.
		/// </summary>
		/// <param name="actionExecutedContext">The action executed context.</param>
		/// <param name="exception">The exception instance.</param>
		public void HandleException(HttpActionExecutedContext actionExecutedContext, Exception exception)
		{
			if (actionExecutedContext == null)
			{
				throw new ArgumentException(nameof(actionExecutedContext));
			}

			if (exception == null)
			{
				throw new ArgumentException(nameof(exception));
			}

			OnHandleException(actionExecutedContext, (TException)exception);
		}

		/// <summary>
		///     Called when an exception needs to be handled.
		/// </summary>
		/// <param name="actionExecutedContext">The action executed context.</param>
		/// <param name="exception">The exception instance.</param>
		protected abstract void OnHandleException(HttpActionExecutedContext actionExecutedContext, TException exception);

		/// <summary>
		///     Creates the error response.
		/// </summary>
		/// <param name="actionExecutedContext">The action executed context.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="httpCode">The HTTP code.</param>
		/// <param name="errorCode">The custom error code</param>
		protected void CreateErrorResponse(HttpActionExecutedContext actionExecutedContext, HttpStatusCode httpCode,
			Exception exception, ErrorCode? errorCode = null)
		{
			var e = HttpContext.Current.IsDebuggingEnabled
				? exception
				: new Exception(exception.Message);

			var errorDetails = new ErrorDetails(e, errorCode, e.Message);
			var response = actionExecutedContext.Request.CreateResponse(httpCode, errorDetails);

			actionExecutedContext.Response = response;
		}

	}
}