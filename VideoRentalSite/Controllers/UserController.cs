using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoRentalSite.Models;

namespace VideoRentalSite.Controllers
{
    public class UserController : Controller
    {
        private VideoRentalEntities db = new VideoRentalEntities();
        // GET: Admin
        public ActionResult Index()
        {
            var item = db.video;
            return View(item);
        }

        public ActionResult PersonalArea()
        {
            return View();
        }

        public ActionResult Bascet()
        {
            return View();
        }
    }
}