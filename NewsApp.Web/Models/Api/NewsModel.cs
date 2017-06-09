using System.Linq;

namespace NewsApp.Web.Models.Api
{
    public class NewsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Prev { get; set; }
        public string Img { get; set; }
        public bool IsFavorite { get; set; }

        public static NewsModel Create(News news)
        {
            return new NewsModel
            {
                Img = news.Image,
                Id = news.NewsId,
                Title = news.Title,
                Prev = news.Description,
                IsFavorite = false
            };
        }

        public static NewsModel Create(News news, ApplicationUser user)
        {
            return new NewsModel
            {
                Img = news.Image,
                Id = news.NewsId,
                Title = news.Title,
                Prev = news.Description,
                IsFavorite = user.UserFavorites.FirstOrDefault(i => i.NewsId == news.NewsId) != null
            };
        }
    }
}