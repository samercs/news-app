using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsApp.Web.Models;

namespace NewsApp.Web.Controllers
{
    [RoutePrefix("api/contact")]
    public class ContactApiController : ApiController
    {
        private ApplicationContext _context = new ApplicationContext();
        [Route("")]
        public IHttpActionResult Post(ContactUs contactUs)
        {
            var contact = new ContactUs
            {
                Email = contactUs.Email,
                Name = contactUs.Name,
                Title = contactUs.Title,
                Message = contactUs.Message,
                AddedDate = DateTime.Now,
                IsRead = false
            };

            _context.ContactUs.Add(contact);
            _context.SaveChanges();
            return Ok(contact);
        }
    }
}
