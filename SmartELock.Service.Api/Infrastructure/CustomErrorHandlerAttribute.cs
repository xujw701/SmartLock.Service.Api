using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using SmartELock.Service.Api.Http;

namespace SmartELock.Service.Api.Infrastructure
{
	public class CustomErrorHandlerAttribute : ExceptionFilterAttribute
	{
		/// <summary>
		///     Handles the exception event.
		/// </summary>
		/// <param name="actionExecutedContext">The context for the action.</param>
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			if (actionExecutedContext.Exception != null)
			{
				if (UnhandledWebExceptionManager.HandleException(actionExecutedContext, actionExecutedContext.Exception))
				{
					var task = actionExecutedContext.Response.Content.ReadAsStringAsync();
					task.Wait();

					// Logger.Info("Web Api Validation [{0}]: {1}", (int)actionExecutedContext.Response.StatusCode, task.Result);
				}
				else
				{

					// Logger.Context.Properties["errorMessage"] = actionExecutedContext.Exception.Message;

					/*
					Logger.Error(
						"Unhandled Exception [500]: {0} {1} {2} : {3}",
						actionExecutedContext.Request.Method,
						actionExecutedContext.Request.RequestUri,
						actionExecutedContext.Exception.Message,
						actionExecutedContext.Exception);
					*/

					var e = HttpContext.Current.IsDebuggingEnabled
						? actionExecutedContext.Exception
						: new Exception("Try again or if problem continues please contact support.");

					actionExecutedContext.Response =
						actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
				}
			}
		}

	}
}