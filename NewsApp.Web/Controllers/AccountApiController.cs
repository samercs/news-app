using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using NewsApp.Web.Core.Identity;
using NewsApp.Web.Models;

namespace NewsApp.Web.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountApiController : ApiController
    {
        private readonly UserManager _userManager = new UserManager(new ApplicationContext());
        

        public AccountApiController()
        {
            
        }

        [Route("")]
        public async Task<IHttpActionResult> Post(RegisterViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                return BadRequest("Invalid user information : " + messages);
            }

            

            var user = new ApplicationUser
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return Ok(result);
            }
            return Ok(true);
        }

        public void AddCookies(string name, string value, DateTime? expiration = null)
        {
            var cookie = new HttpCookie(name)
            {
                Value = value,
                Secure = false,
                HttpOnly = true
            };

            if (expiration.HasValue)
            {
                cookie.Expires = expiration.Value;
            }
        }
    }
}
