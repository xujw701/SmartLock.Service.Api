using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SmartELock.Service.Api.Filters
{
	/// <summary>
	///    Custom action filter to valid model state before controller action is invoked.
	///    Inspired from: http://www.asp.net/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
	/// </summary>
	public class ValidateModelAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (actionContext.ModelState.IsValid == false)
			{
				actionContext.Response = actionContext.Request.CreateErrorResponse(
					HttpStatusCode.BadRequest, actionContext.ModelState);
			}
		}
	}
}