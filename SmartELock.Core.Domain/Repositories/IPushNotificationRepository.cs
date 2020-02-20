using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.PushNotification;
using System.Net;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IPushNotificationRepository
    {
        Task<string> CreateRegistrationIdAsync(string handle);
        Task CreateOrUpdateRegistrationAsync(string id, DeviceRegistration deviceUpdate);
        Task DeleteRegistrationAsync(string id);
        Task<HttpStatusCode> SendNotification(string pns, string title, string body, string tag, string[] tags, string dateTime = null);
    }
}
