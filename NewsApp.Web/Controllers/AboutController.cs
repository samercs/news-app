using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsApp.Web.Models;

namespace NewsApp.Web.Controllers
{
    [RoutePrefix("api/about")]
    public class AboutController : ApiController
    {
        private ApplicationContext _context = new ApplicationContext();

        [Route("")]
        public IHttpActionResult Get()
        {
            var about = _context.Pages.FirstOrDefault(i => i.PageId == 1);
            if (about != null)
            {
                return Ok(about);
            }

            return BadRequest("Page not found");
        }
    }
}
