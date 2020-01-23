using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    public class BaseController : ApiController
    {
        private const string UserIdKey = "UserId";
        private const string BearerKey = "Bearer";

        protected int UserId { get; private set; }

        // Only validate admin's token
        protected async Task ValidateUserToken(HttpRequestHeaders headers)
        {
        }

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