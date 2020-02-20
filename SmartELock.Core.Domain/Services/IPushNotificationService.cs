using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.PushNotification;
using System;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Services
{
    public interface IPushNotificationService
    {
        Task<string> CreateRegistrationIdAsync(string handle);
        Task CreateOrUpdateRegistrationAsync(string id, DeviceRegistration deviceUpdate);
        Task DeleteRegistrationAsync(string id);
        Task SendNotification(string title, string message, string tag, string[] tags);
    }
}
