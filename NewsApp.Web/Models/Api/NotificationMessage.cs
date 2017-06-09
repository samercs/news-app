using System.Collections.Generic;

namespace NewsApp.Web.Models.Api
{
    public class NotificationMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Tags { get; set; }

    }
}