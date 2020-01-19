using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
	public class BaseController : ApiController
	{
		protected IHttpActionResult CreatedResult<T>(T t)
		{
			if (ReferenceEquals(t, null))
			{
				return NotFound();
			}

			return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, t));
		}
	}
}