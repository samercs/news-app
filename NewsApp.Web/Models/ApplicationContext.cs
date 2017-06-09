using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NewsApp.Web.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<News> Newses { get; set; }
        public IDbSet<Page> Pages { get; set; }
        public IDbSet<UserFavorite> UserFavorites { get; set; }
        public IDbSet<ContactUs> ContactUs { get; set; }
        public IDbSet<Issue> Issues { get; set; }
        public ApplicationContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(prop => prop.HasPrecision(18, 3));


            base.OnModelCreating(modelBuilder);
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {

                throw;
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            try
            {
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {

                throw;
            }
        }


    }
}