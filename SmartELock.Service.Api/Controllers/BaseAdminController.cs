using SmartELock.Core.Domain.Services;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    public class BaseAdminController : BaseController
    {
        private const string SuperAdminIdKey = "SuperAdminId";
        private const string BearerKey = "Bearer";

        private readonly ISuperAdminService _superAdminService;

        protected int AdminId { get; private set; }

        public BaseAdminController(ISuperAdminService superAdminService)
        {
            _superAdminService = superAdminService;
        }

        // Only validate admin's token
        protected async Task ValidateAdminToken(HttpRequestHeaders headers)
        {
            if (!headers.Contains(SuperAdminIdKey) || !headers.Contains(BearerKey))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var adminId = int.Parse(headers.FirstOrDefault(header => string.Equals(header.Key, SuperAdminIdKey)).Value.FirstOrDefault());
            var token = headers.FirstOrDefault(header => string.Equals(header.Key, BearerKey)).Value.FirstOrDefault();

            var result = await _superAdminService.CheckToken(adminId, token);

            if (!result)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            AdminId = adminId;
        }

        // Validate both admin and user's token
        protected async Task ValidAdminOrUserToken(HttpRequestHeaders headers)
        { 
        }
    }
}