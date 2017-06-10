using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure.NotificationHubs;
using NewsApp.Web.Core.Services;
using NewsApp.Web.Models;

namespace NewsApp.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class NewsController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        private readonly NotificationHubClient _notificationHubClient = NotificationHubClient.CreateClientFromConnectionString(
                    ConfigurationManager.AppSettings["AzureNotificationHubConnectionString"],
                    ConfigurationManager.AppSettings["AzureNotificationHubName"]);

        // GET: Admin/News1
        public ActionResult Index()
        {
            return View(db.Newses.OrderByDescending(i => i.NewsId).ToList());
        }

        // GET: Admin/News1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.Newses.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/News1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "NewsId,Title,Image,Description")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Newses.Add(news);
                db.SaveChanges();
                var messageString = "{\"data\": {\"message\": \"New News Was Added\"} }";
                var result = await _notificationHubClient.SendGcmNativeNotificationAsync(messageString);
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: Admin/News1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.Newses.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "NewsId,Title,Image,Description")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: Admin/News1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.Newses.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.Newses.Find(id);
            db.Newses.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
