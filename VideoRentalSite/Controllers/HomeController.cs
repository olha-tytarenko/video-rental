using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoRentalSite.Models;

namespace VideoRentalSite.Controllers
{
    public class HomeController : Controller
    {

        private VideoRentalEntities db = new VideoRentalEntities();
        public ActionResult Index()
        {
            var item = db.video;
            return View(item);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(user User)
        {
            if (ModelState.IsValid)
            {
                int id = (from m in db.user select m.user_id).ToList().Last() + 1;
                User.user_id = id;
                User.user_status = "user";
                db.user.Add(User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user User)
        {
            if (ModelState.IsValid)
            {
                var details = (from userlist in db.user
                               where (userlist.user_email == User.user_email || userlist.user_name == User.user_name) &&
                               userlist.user_password == User.user_password
                               select new
                               {
                                   userlist.user_id,
                                   userlist.user_name,
                                   userlist.user_status                              
                               }).ToList();
                if (details.FirstOrDefault() != null)
                {
                    Session["user_id"] = details.FirstOrDefault().user_id;
                    Session["user_name"] = details.FirstOrDefault().user_name;
                    Session["user_status"] = details.FirstOrDefault().user_status;
                    if (details.FirstOrDefault().user_status == "admin")
                    {
                        return RedirectToAction("AdminIndex", "Home");
                    }
                    return RedirectToAction("UserIndex", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Credentials");
            }
            return View(User);
        }
        

        public ActionResult AdminIndex()
        {
            var item = db.video;
            return View(item);
        }

        public ActionResult UserIndex()
        {
            var item = db.video;
            return View(item);
        }
        public ActionResult AdminEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            video video = db.video.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Start()
        {
            return View();
        }
        
        public ActionResult PersonalArea()
        {
            return View();
        }


        public ActionResult Basket(int? id)
        {
            order order = db.order.Find(id);
            return View(order);
        }

    }
}