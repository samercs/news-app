using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace NewsApp.Web.Core.Services
{
    public class PushNotificationService
    {
        private readonly NotificationHubClient _notificationHubClient;

        public PushNotificationService()
        {
            _notificationHubClient = NotificationHubClient.CreateClientFromConnectionString(
                ConfigurationManager.AppSettings["AzureNotificationHubConnectionString"],
                ConfigurationManager.AppSettings["AzureNotificationHubName"]);
        }

        public async Task<RegistrationDescription> Register(string platform, string registerId, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                await _notificationHubClient.DeleteRegistrationAsync(key);
            }

            switch (platform)
            {
                case "android":
                    return await _notificationHubClient.CreateGcmNativeRegistrationAsync(registerId);
                case "ios":
                    return await _notificationHubClient.CreateAppleNativeRegistrationAsync(registerId);
                default:
                    throw new Exception("Not supported platform.");
            }
        }

        public async Task<RegistrationDescription> Register(string platform, string registerId, string key,
            IEnumerable<string> tags)
        {
            if (!string.IsNullOrEmpty(key))
            {
                await _notificationHubClient.DeleteRegistrationAsync(key);
            }

            switch (platform)
            {
                case "android":
                    return await _notificationHubClient.CreateGcmNativeRegistrationAsync(registerId, tags);
                case "ios":
                    return await _notificationHubClient.CreateAppleNativeRegistrationAsync(registerId, tags);
                default:
                    throw new Exception("Not supported platform.");
            }
        }

        public async Task Send(string platform, string message, string title, IEnumerable<string> tags)
        {
            var messageString = "{\"data\": {\"message\": \"" + message + "\", \"title\": \"" + title + "\"} }";

            switch (platform)
            {
                case "android":
                    await _notificationHubClient.SendGcmNativeNotificationAsync(messageString, tags);
                    break;
                case "ios":
                    await _notificationHubClient.SendAppleNativeNotificationAsync(messageString, tags);
                    break;
                default:
                    throw new Exception("Not supported platform.");
            }
        }

        public async Task SendIndividual(string message, string title, string tag)
        {
            var registerations = await _notificationHubClient.GetRegistrationsByTagAsync(tag, 100);
            foreach (
                var platform in
                registerations.Select(registeration => registeration.Tags.Contains("android") ? "android" : "ios"))
            {
                await Send(platform, message, title, new List<string> { tag });
            }
        }

        public async Task<int> DeleteRegistration()
        {
            var allRegister = await _notificationHubClient.GetAllRegistrationsAsync(100);
            int i = 0;
            foreach (var registrationDescription in allRegister)
            {
                await _notificationHubClient.DeleteRegistrationAsync(registrationDescription.RegistrationId);
                ++i;
            }
            return i;
        }
    }
}