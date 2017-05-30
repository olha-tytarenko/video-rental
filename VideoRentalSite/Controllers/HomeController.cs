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
        



        public ActionResult Basket()

        {
            return View();
        }

        public ActionResult VideoDetails(int? id)
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

        public EmptyResult AddIntoBasket()
        {
            int id = Int32.Parse(Request.Params[0]);
            int orderId = (from m in db.order select m.order_id).ToList().Last() + 1;
            order order = new order();
            order.order_id = orderId;
            order.order_data = DateTime.Now;
            order.order_id_user = Int32.Parse(Session["user_id"].ToString());
            order.order_status = "basket";
            db.order.Add(order);
            db.SaveChanges();
            videolist videolist = new videolist();
            videolist.videolist_id = (from m in db.videolist select m.videolist_id).ToList().Last() + 1;
            videolist.videolist_id_order = orderId;
            videolist.videolist_id_video = id;
            videolist.videolist_quantity = 1;
            db.videolist.Add(videolist);
            db.SaveChanges();

            return new EmptyResult();
        }

        public EmptyResult DeleteFromBasket()
        {
            int orderId = Int32.Parse(Request.Params[0].ToString());
            videolist videolist = db.videolist.SingleOrDefault(vl => vl.videolist_id_order == orderId);
            db.videolist.Remove(videolist);
            db.SaveChanges();

            return new EmptyResult();
        }

        public EmptyResult MakeOrder()
        {
            string [] ids = Request["ids"].Split(',');
            DateTime dateNow = DateTime.Now;
            int newOrderId = (from m in db.order select m.order_id).ToList().Last() + 1;

            order newOrder = new order();
            newOrder.order_id = newOrderId;
            newOrder.order_data = dateNow;
            newOrder.order_id_user = Int32.Parse(Session["user_id"].ToString());
            newOrder.order_status = "processing";
            db.order.Add(newOrder);
            db.SaveChanges();

            for (int i = 0; i < ids.Length; i++)
            {
                int orderId = Int32.Parse(ids[i]);
                videolist videolist = db.videolist.SingleOrDefault(vl => vl.videolist_id_order == orderId);
                videolist.videolist_id_order = newOrderId;

                order order = db.order.SingleOrDefault(o => o.order_id == orderId);
                db.order.Remove(order);

                db.SaveChanges();
            }
            


            return new EmptyResult();
        }



        public ActionResult PersonalArea()
        {
            return View();
        }

        public ActionResult OrderDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult AddVideo()
        {
            return View();
        }

        public EmptyResult AddNewVideo()
        {
            video video = new video();
            int video_id = (from m in db.video select m.video_id).ToList().Last() + 1;
            video.video_id = video_id;
            video.video_name = Request["name"];
            video.video_year = Int32.Parse(Request["year"]);
            video.video_description = Request["description"];
            video.video_price_per_day = Double.Parse(Request["price"]);
            video.video_country = Request["country"];
            video.video_quantity = Int32.Parse(Request["quantity"]);
            db.video.Add(video);
            db.SaveChanges();

            string [] genre = Request["genre"].Split(',');
            int videoGenreId = (from m in db.videogenre select m.videogenre_id).ToList().Last();
            for (int i = 0; i < genre.Length; i++)
            {
                videogenre videoGenre = new videogenre();
                videoGenre.videogenre_id = ++videoGenreId;
                videoGenre.videogenre_id_video = video_id;
                videoGenre.videogenre_id_genre = Int32.Parse(genre[i]);

                db.videogenre.Add(videoGenre);
                db.SaveChanges();

            }

            string producer = Request["producer"];
            producer findProducer = db.producer.SingleOrDefault(p => p.producer_name == producer);

            videoproducer vp = new videoproducer();
            vp.videoproducer_id = (from m in db.videoproducer select m.videoproducer_id).ToList().Last() + 1;
            vp.videoproducer_id_video = video_id;
            if (findProducer == null)
            {
                producer pr = new producer();
                int id = (from m in db.producer select m.producer_id).ToList().Last() + 1;
                pr.producer_id = id;
                pr.producer_name = producer;

                db.producer.Add(pr);
                db.SaveChanges();


                vp.videoproducer_id_producer = id;
                

            } else
            {
                vp.videoproducer_id_producer = findProducer.producer_id;
                
            }

            db.SaveChanges();
            return new EmptyResult();
        }

        public EmptyResult AddCover()
        {
            string path = "";
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    path = "/Content/img/" + fileName;
                    // сохраняем файл в папку Files в проекте
                    upload.SaveAs(Server.MapPath(path));
                    
                }
            }

            int videoId = (from m in db.video select m.video_id).ToList().Last();
            video lastVideo = db.video.SingleOrDefault(v => v.video_id == videoId);
            lastVideo.video_cover_url = path;
            db.SaveChanges();
            return new EmptyResult();
        }

    }
}