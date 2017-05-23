using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoRentalSite.Models;

namespace VideoRentalSite.Controllers
{
    public class videolistsController : Controller
    {
        private VideoRentalEntities db = new VideoRentalEntities();

        // GET: videolists
        public ActionResult Index()
        {
            var videolist = db.videolist.Include(v => v.order).Include(v => v.video);
            return View(videolist.ToList());
        }

        // GET: videolists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            videolist videolist = db.videolist.Find(id);
            if (videolist == null)
            {
                return HttpNotFound();
            }
            return View(videolist);
        }

        // GET: videolists/Create
        public ActionResult Create()
        {
            ViewBag.videolist_id_order = new SelectList(db.order, "order_id", "order_status");
            ViewBag.videolist_id_video = new SelectList(db.video, "video_id", "video_name");
            return View();
        }

        // POST: videolists/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "videolist_id,videolist_id_order,videolist_id_video,videolist_quantity")] videolist videolist)
        {
            if (ModelState.IsValid)
            {
                db.videolist.Add(videolist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.videolist_id_order = new SelectList(db.order, "order_id", "order_status", videolist.videolist_id_order);
            ViewBag.videolist_id_video = new SelectList(db.video, "video_id", "video_name", videolist.videolist_id_video);
            return View(videolist);
        }

        // GET: videolists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            videolist videolist = db.videolist.Find(id);
            if (videolist == null)
            {
                return HttpNotFound();
            }
            ViewBag.videolist_id_order = new SelectList(db.order, "order_id", "order_status", videolist.videolist_id_order);
            ViewBag.videolist_id_video = new SelectList(db.video, "video_id", "video_name", videolist.videolist_id_video);
            return View(videolist);
        }

        // POST: videolists/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "videolist_id,videolist_id_order,videolist_id_video,videolist_quantity")] videolist videolist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videolist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.videolist_id_order = new SelectList(db.order, "order_id", "order_status", videolist.videolist_id_order);
            ViewBag.videolist_id_video = new SelectList(db.video, "video_id", "video_name", videolist.videolist_id_video);
            return View(videolist);
        }

        // GET: videolists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            videolist videolist = db.videolist.Find(id);
            if (videolist == null)
            {
                return HttpNotFound();
            }
            return View(videolist);
        }

        // POST: videolists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            videolist videolist = db.videolist.Find(id);
            db.videolist.Remove(videolist);
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
