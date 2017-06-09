using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsApp.Web.Models;

namespace NewsApp.Web.Core.Identity
{
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }

        public RoleManager(ApplicationContext dataContextFactory)
            : base(new RoleStore<IdentityRole>((DbContext)dataContextFactory))
        {
        }
    }
}