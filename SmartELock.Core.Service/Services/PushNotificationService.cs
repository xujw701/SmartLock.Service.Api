using SmartELock.Core.Domain.Models.PushNotification;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Services;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IPushNotificationRepository _pushNotificationRepository;

        public PushNotificationService(IPushNotificationRepository pushNotificationRepository)
        {
            _pushNotificationRepository = pushNotificationRepository;
        }

        public async Task CreateOrUpdateRegistrationAsync(string id, DeviceRegistration deviceUpdate)
        {
            await _pushNotificationRepository.CreateOrUpdateRegistrationAsync(id, deviceUpdate);
        }

        public async Task<string> CreateRegistrationIdAsync(string handle)
        {
            return await _pushNotificationRepository.CreateRegistrationIdAsync(handle);
        }

        public async Task DeleteRegistrationAsync(string id)
        {
            await _pushNotificationRepository.DeleteRegistrationAsync(id);
        }

        public async Task SendNotification(string title, string message, string tag, string[] tags)
        {
            await SendNotification("fcm", title, message, tag, tags);
            //await SendNotification("apns", title, message, tag, tags);
        }

        private async Task SendNotification(string pns, string title, string message, string tag, string[] tags)
        {
            await _pushNotificationRepository.SendNotification(pns, title, message, tag, tags);
        }
    }
}
