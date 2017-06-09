using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Azure.NotificationHubs;
using NewsApp.Web.Core.Services;
using NewsApp.Web.Models.Api;
using Newtonsoft.Json;

namespace NewsApp.Web.Controllers
{
    [RoutePrefix("api/push-notification")]
    public class PushNotificationController : ApiController
    {
        private readonly NotificationHubClient _notificationHubClient;
        private readonly PushNotificationService _pushNotificationService;

        public PushNotificationController()
        {
            _notificationHubClient =
                NotificationHubClient.CreateClientFromConnectionString(
                    ConfigurationManager.AppSettings["AzureNotificationHubConnectionString"],
                    ConfigurationManager.AppSettings["AzureNotificationHubName"]);
            _pushNotificationService = new PushNotificationService();
        }

        [Route("register"), HttpPost]
        public async Task<IHttpActionResult> Register(NotificationRegisterModel model)
        {
            var tags = new List<string>
            {
                model.Platform
            };

            if (!string.IsNullOrWhiteSpace(model.UserId))
            {
                tags.Add($"user:{model.UserId}");
            }

            Trace.TraceInformation($"Registering notification: {JsonConvert.SerializeObject(model)}");

            var result = await _pushNotificationService.Register(model.Platform, model.RegisterId, model.Key, tags);

            Trace.TraceInformation($"Registration result: {JsonConvert.SerializeObject(result)}");

            return Ok(result.RegistrationId);
        }

        [Route("send"), HttpPost]
        public async Task<IHttpActionResult> Send(NotificationMessage message)
        {
            var messageString = "{\"data\": {\"message\": \"" + message.Message + "\", \"title\": \"" + message.Title +
                                "\"} }";
            var result = await _notificationHubClient.SendGcmNativeNotificationAsync(messageString, message.Tags);
            return Ok("Notification has been send successfully.");
        }

        [Route("delete-registration"), HttpPost]
        public async Task<IHttpActionResult> DeleteRegistration()
        {
            int deleteCount = await _pushNotificationService.DeleteRegistration();
            return Ok($"{deleteCount} registration has been delete");
        }
    }
}
