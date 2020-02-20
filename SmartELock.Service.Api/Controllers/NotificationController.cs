using SmartELock.Core.Domain.Models.PushNotification;
using SmartELock.Core.Domain.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    [RoutePrefix("api/v1/notification")]
    public class NotificationController : BaseController
    {
        private readonly IPushNotificationService _pushNotificationService;

        public NotificationController(IAuthorizationService authorizationService,  IPushNotificationService pushNotificationService) : base(authorizationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        [HttpPost]
        [Route("")]
        // This gets or creates a registration id
        public async Task<string> Post(NotificationHandle notificationHandle)
        {
            await ValidateToken(Request.Headers);

            return await _pushNotificationService.CreateRegistrationIdAsync(notificationHandle.Handle);
        }

        [HttpPut]
        [Route("{id}")]
        // Updates a registration (with provided channelURI) at the specified id
        public async Task<IHttpActionResult> Put(string id, DeviceRegistration deviceUpdate)
        {
            await ValidateToken(Request.Headers);

            await _pushNotificationService.CreateOrUpdateRegistrationAsync(id, deviceUpdate);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(string id)
        {
            await ValidateToken(Request.Headers);

            await _pushNotificationService.DeleteRegistrationAsync(id);
            return Ok();
        }
    }
}