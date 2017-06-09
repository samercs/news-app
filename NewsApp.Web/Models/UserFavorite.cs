using System.ComponentModel.DataAnnotations;

namespace NewsApp.Web.Models
{
    public class UserFavorite
    {
        public int UserFavoriteId { get; set; }
        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        public int NewsId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual News News { get; set; }
    }
}