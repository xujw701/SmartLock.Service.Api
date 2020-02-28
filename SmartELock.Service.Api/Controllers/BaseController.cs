using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Services;
using System;
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
        private const int MaxUploadSize = 1024 * 1024 * 5; // 5 Mb

        private readonly IAuthorizationService _authorizationService;

        protected SuperAdmin CurrentSuperAdmin { get; private set; }
        protected User CurrentUser { get; private set; }

        protected int AdminId => CurrentSuperAdmin != null ? CurrentSuperAdmin.SuperAdminId : 0;
        protected int UserId => CurrentUser != null ? CurrentUser.UserId : 0;

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

                if (!result.Item1)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                CurrentSuperAdmin = result.Item2;
            }
            else if (headers.Contains(UserIdKey) && headers.Contains(BearerKey) && !adminOnly)
            {
                var userId = int.Parse(headers.FirstOrDefault(header => string.Equals(header.Key, UserIdKey)).Value.FirstOrDefault());
                var token = headers.FirstOrDefault(header => string.Equals(header.Key, BearerKey)).Value.FirstOrDefault();

                var result = await _authorizationService.CheckUserToken(userId, token);

                if (!result.Item1)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                CurrentUser = result.Item2;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        protected async Task<byte[]> GetBodyBytes()
        {
            using (var ms = new System.IO.MemoryStream())
            {
                await Request.Content.CopyToAsync(ms);

                var result = ms.ToArray();

                if (result.Length > MaxUploadSize)
                {
                    throw new Exception("Upload filesize exceeds " + MaxUploadSize + "bytes.");
                }

                return result;
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