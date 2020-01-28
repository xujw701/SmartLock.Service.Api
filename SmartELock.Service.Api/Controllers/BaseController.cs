using SmartELock.Core.Domain.Services;
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
        private const string SuperAdminIdKey = "SuperAdminId";
        private const string UserIdKey = "UserId";
        private const string BearerKey = "Bearer";

        private readonly IAuthorizationService _authorizationService;

        protected int AdminId { get; private set; }
        protected int UserId { get; private set; }

        public BaseController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        protected async Task ValidateToken(HttpRequestHeaders headers, bool adminOnly = false)
        {
            if (headers.Contains(SuperAdminIdKey) && headers.Contains(BearerKey))
            {
                var adminId = int.Parse(headers.FirstOrDefault(header => string.Equals(header.Key, SuperAdminIdKey)).Value.FirstOrDefault());
                var token = headers.FirstOrDefault(header => string.Equals(header.Key, BearerKey)).Value.FirstOrDefault();

                var result = await _authorizationService.CheckAdminToken(adminId, token);

                if (!result)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                AdminId = adminId;
            }
            else if (headers.Contains(UserIdKey) && headers.Contains(BearerKey) && !adminOnly)
            {
                var userId = int.Parse(headers.FirstOrDefault(header => string.Equals(header.Key, UserIdKey)).Value.FirstOrDefault());
                var token = headers.FirstOrDefault(header => string.Equals(header.Key, BearerKey)).Value.FirstOrDefault();

                var result = await _authorizationService.CheckUserToken(userId, token);

                if (!result)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                UserId = userId;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
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