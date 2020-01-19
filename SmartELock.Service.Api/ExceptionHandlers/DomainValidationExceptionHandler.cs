using System.Net;
using System.Web.Http.Filters;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Service.Api.Http;

namespace SmartELock.Service.Api.ExceptionHandlers
{
	public class DomainValidationExceptionHandler : WebExceptionHandler<DomainValidationException>
	{

		/// <summary>
		///     Called when a conflict exception needs to be handled.
		/// </summary>
		/// <param name="actionExecutedContext">The action executed context.</param>
		/// <param name="exception">The exception instance.</param>
		protected override void OnHandleException(HttpActionExecutedContext actionExecutedContext,
			DomainValidationException exception)
		{
			CreateErrorResponse(actionExecutedContext, HttpStatusCode.BadRequest, exception, exception.Code);
		}
	}
}