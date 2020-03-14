using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using SmartELock.Core.Domain.Models.PushNotification;
using SmartELock.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Repositories.Repositories
{
    public class PushNotificationRepository : IPushNotificationRepository
    {
        private NotificationHubClient _hub;

        public PushNotificationRepository()
        {
            _hub = NotificationHubClient.CreateClientFromConnectionString(ConfigurationManager.AppSettings["NotificationHubConnection"], ConfigurationManager.AppSettings["NotificationHubName"]);
        }

        public async Task<string> CreateRegistrationIdAsync(string handle)
        {
            string newRegistrationId = null;

            // make sure there are no existing registrations for this push handle (used for iOS and Android)
            // hanle means device token ( not registration id !! )
            if (handle != null)
            {
                var registrations = await _hub.GetRegistrationsByChannelAsync(handle, 100);

                foreach (RegistrationDescription registration in registrations)
                {
                    if (newRegistrationId == null)
                    {
                        newRegistrationId = registration.RegistrationId;
                    }
                    else
                    {
                        await _hub.DeleteRegistrationAsync(registration);
                    }
                }
            }

            if (newRegistrationId == null)
                newRegistrationId = await _hub.CreateRegistrationIdAsync();

            return newRegistrationId;
        }

        public async Task CreateOrUpdateRegistrationAsync(string id, DeviceRegistration deviceUpdate)
        {
            RegistrationDescription registration = null;
            switch (deviceUpdate.Platform)
            {
                case "mpns":
                    registration = new MpnsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "wns":
                    registration = new WindowsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "apns":
                    registration = new AppleRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "fcm":
                    registration = new FcmRegistrationDescription(deviceUpdate.Handle);
                    break;
            }

            registration.RegistrationId = id;

            // add check if user is allowed to add these tags
            registration.Tags = new HashSet<string>(deviceUpdate.Tags);

            // Remove the existing registration with same traveller id tag
            await RemoveExistingRegistrationAsync(deviceUpdate);

            await CreateOrUpdateRegistrationAsync(registration);
        }

        public async Task DeleteRegistrationAsync(string id)
        {
            await _hub.DeleteRegistrationAsync(id);
        }

        public async Task<HttpStatusCode> SendNotification(string pns, string title, string body, string tag, string[] tags, string dateTime = null)
        {
            NotificationOutcome outcome = null;
            var ret = HttpStatusCode.InternalServerError;
            switch (pns.ToLower())
            {
                case "wns":
                    // Windows 8.1 / Windows Phone 8.1
                    var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">" +
                                body + "</text></binding></visual></toast>";
                    outcome = await _hub.SendWindowsNativeNotificationAsync(toast, tags);
                    break;
                case "apns":
                    // iOS
                    var alert = "{\"aps\":{\"alert\":{\"title\":\"" + title + "\",\"body\":\"" + body + "\",\"tag\":\"" + tag + "\",\"dateTime\":\"" + dateTime + "\"}}}";

                    foreach (var t in tags)
                    {
                        var headers = new Dictionary<string, string>
                        {
                            { "apns-push-type", "alert" },
                            //{ "apns-priority", "5" },
                        };
                        var notification = new AppleNotification(alert, headers);
                        outcome = await _hub.SendNotificationAsync(notification);
                    }
                    break;
                case "fcm":
                    // Android
                    var notif = "{\"data\":{\"title\":\"" + title + "\",\"body\":\"" + body + "\",\"tag\":\"" + tag + "\",\"dateTime\":\"" + dateTime + "\"}}";
                    outcome = await _hub.SendFcmNativeNotificationAsync(notif, tags);
                    break;
            }

            if (outcome != null)
            {
                if (!((outcome.State == NotificationOutcomeState.Abandoned) ||
                    (outcome.State == NotificationOutcomeState.Unknown)))
                {
                    ret = HttpStatusCode.OK;
                }
            }
            return ret;
        }

        private async Task CreateOrUpdateRegistrationAsync(RegistrationDescription registration)
        {
            try
            {
                await _hub.CreateOrUpdateRegistrationAsync(registration);
            }
            catch (MessagingException e)
            {
                ReturnGoneIfHubResponseIsGone(e);
            }
        }

        private static void ReturnGoneIfHubResponseIsGone(MessagingException e)
        {
            var webex = e.InnerException as WebException;
            if (webex.Status == WebExceptionStatus.ProtocolError)
            {
                var response = (HttpWebResponse)webex.Response;
                if (response.StatusCode == HttpStatusCode.Gone)
                    throw new HttpRequestException(HttpStatusCode.Gone.ToString());
            }
        }

        private async Task RemoveExistingRegistrationAsync(DeviceRegistration deviceRegistration)
        {
            var userId = deviceRegistration.Tags.Where(tag => int.TryParse(tag, out int result)).FirstOrDefault();
            if (string.IsNullOrEmpty(userId)) return;

            var registrations = await _hub.GetRegistrationsByTagAsync(userId, 100);
            foreach (var registration in registrations)
            {
                await _hub.DeleteRegistrationAsync(registration);
            }
        }
    }
}
