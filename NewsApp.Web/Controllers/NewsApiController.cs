using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using NewsApp.Web.Core.Identity;
using NewsApp.Web.Models;
using NewsApp.Web.Models.Api;

namespace NewsApp.Web.Controllers
{
    [RoutePrefix("api/news")]
    public class NewsApiController : ApiController
    {
        private readonly ApplicationContext _context = new ApplicationContext();
        private readonly UserService _userService = new UserService(new ApplicationContext());
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var news = _context.Newses.ToList();
            if (!User.Identity.IsAuthenticated)
            {
                return Ok(news.Select(NewsModel.Create));
            }
            var user = await _userService.GetUserById(User.Identity.GetUserId());
            return Ok(news.Select(i => NewsModel.Create(i, user)));

        }

        [Route("favorite")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetFavorite()
        {
            var userId = User.Identity.GetUserId();
            var user = await _userService.GetUserById(User.Identity.GetUserId());
            var userFavoriteNewsIds = user.UserFavorites.Select(i => i.NewsId);
            var news = _context.Newses.Where(i => userFavoriteNewsIds.Contains(i.NewsId)).ToList();
            return Ok(news.Select(i => NewsModel.Create(i, user)));

        }

        [Route("favorite/{newsId:int}")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult AddToFavorite(int newsId)
        {
            var userId = User.Identity.GetUserId();
            var oldUserFavorite = _context.UserFavorites.FirstOrDefault(i => i.NewsId == newsId && i.UserId == userId);
            if (oldUserFavorite == null)
            {
                var userFavorite = new UserFavorite
                {
                    NewsId = newsId,
                    UserId = userId
                };
                _context.UserFavorites.Add(userFavorite);
                _context.SaveChanges();
                return Ok(true);
            }

            _context.UserFavorites.Remove(oldUserFavorite);
            _context.SaveChanges();
            return Ok(false);

        }

        [Route("search/{query}")]
        [HttpGet]
        public async Task<IHttpActionResult> Search(string query)
        {
            var news = _context.Newses.Where(i => i.Title.Contains(query) || i.Description.Contains(query)).ToList();
            if (!User.Identity.IsAuthenticated)
            {
                return Ok(news.Select(NewsModel.Create));
            }
            var user = await _userService.GetUserById(User.Identity.GetUserId());
            return Ok(news.Select(i => NewsModel.Create(i, user)));

        }


    }
}