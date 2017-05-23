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
    public class videoproducersController : Controller
    {
        private VideoRentalEntities db = new VideoRentalEntities();

        // GET: videoproducers
        public ActionResult Index()
        {
            var videoproducer = db.videoproducer.Include(v => v.producer).Include(v => v.video);
            return View(videoproducer.ToList());
        }

        // GET: videoproducers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            videoproducer videoproducer = db.videoproducer.Find(id);
            if (videoproducer == null)
            {
                return HttpNotFound();
            }
            return View(videoproducer);
        }

        // GET: videoproducers/Create
        public ActionResult Create()
        {
            ViewBag.videoproducer_id_producer = new SelectList(db.producer, "producer_id", "producer_name");
            ViewBag.videoproducer_id_video = new SelectList(db.video, "video_id", "video_name");
            return View();
        }

        // POST: videoproducers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "videoproducer_id,videoproducer_id_producer,videoproducer_id_video")] videoproducer videoproducer)
        {
            if (ModelState.IsValid)
            {
                db.videoproducer.Add(videoproducer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.videoproducer_id_producer = new SelectList(db.producer, "producer_id", "producer_name", videoproducer.videoproducer_id_producer);
            ViewBag.videoproducer_id_video = new SelectList(db.video, "video_id", "video_name", videoproducer.videoproducer_id_video);
            return View(videoproducer);
        }

        // GET: videoproducers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            videoproducer videoproducer = db.videoproducer.Find(id);
            if (videoproducer == null)
            {
                return HttpNotFound();
            }
            ViewBag.videoproducer_id_producer = new SelectList(db.producer, "producer_id", "producer_name", videoproducer.videoproducer_id_producer);
            ViewBag.videoproducer_id_video = new SelectList(db.video, "video_id", "video_name", videoproducer.videoproducer_id_video);
            return View(videoproducer);
        }

        // POST: videoproducers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "videoproducer_id,videoproducer_id_producer,videoproducer_id_video")] videoproducer videoproducer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoproducer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.videoproducer_id_producer = new SelectList(db.producer, "producer_id", "producer_name", videoproducer.videoproducer_id_producer);
            ViewBag.videoproducer_id_video = new SelectList(db.video, "video_id", "video_name", videoproducer.videoproducer_id_video);
            return View(videoproducer);
        }

        // GET: videoproducers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            videoproducer videoproducer = db.videoproducer.Find(id);
            if (videoproducer == null)
            {
                return HttpNotFound();
            }
            return View(videoproducer);
        }

        // POST: videoproducers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            videoproducer videoproducer = db.videoproducer.Find(id);
            db.videoproducer.Remove(videoproducer);
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
