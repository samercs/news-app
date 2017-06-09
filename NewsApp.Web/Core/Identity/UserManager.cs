using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsApp.Web.Models;

namespace NewsApp.Web.Core.Identity
{
    public class UserManager : UserManager<ApplicationUser>
    {
        public UserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            InitUserManager();
        }

        public UserManager(ApplicationContext dataContextFactory)
            : base(new UserStore<ApplicationUser>((DbContext)dataContextFactory))
        {
            InitUserManager();
        }

        private void InitUserManager()
        {
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(10);
            MaxFailedAccessAttemptsBeforeLockout = 10;


        }
    }
}