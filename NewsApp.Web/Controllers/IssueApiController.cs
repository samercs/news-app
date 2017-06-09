using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsApp.Web.Models;

namespace NewsApp.Web.Controllers
{
    [RoutePrefix("api/issue")]
    public class IssueApiController : ApiController
    {

        private ApplicationContext _context = new ApplicationContext();

        [Route("")]
        public IHttpActionResult Post(Issue issue)
        {
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return BadRequest(messages);
            }

            _context.Issues.Add(issue);
            _context.SaveChanges();
            return Ok(issue);
        }
    }
}
