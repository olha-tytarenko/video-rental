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
    public class videogenresController : Controller
    {
        private VideoRentalEntities db = new VideoRentalEntities();

        // GET: videogenres
        public ActionResult Index()
        {
            var videogenre = db.videogenre.Include(v => v.genre).Include(v => v.video);
            return View(videogenre.ToList());
        }

        // GET: videogenres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            videogenre videogenre = db.videogenre.Find(id);
            if (videogenre == null)
            {
                return HttpNotFound();
            }
            return View(videogenre);
        }

        // GET: videogenres/Create
        public ActionResult Create()
        {
            ViewBag.videogenre_id_genre = new SelectList(db.genre, "genre_id", "genre_name");
            ViewBag.videogenre_id_video = new SelectList(db.video, "video_id", "video_name");
            return View();
        }

        // POST: videogenres/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "videogenre_id,videogenre_id_genre,videogenre_id_video")] videogenre videogenre)
        {
            if (ModelState.IsValid)
            {
                db.videogenre.Add(videogenre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.videogenre_id_genre = new SelectList(db.genre, "genre_id", "genre_name", videogenre.videogenre_id_genre);
            ViewBag.videogenre_id_video = new SelectList(db.video, "video_id", "video_name", videogenre.videogenre_id_video);
            return View(videogenre);
        }

        // GET: videogenres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            videogenre videogenre = db.videogenre.Find(id);
            if (videogenre == null)
            {
                return HttpNotFound();
            }
            ViewBag.videogenre_id_genre = new SelectList(db.genre, "genre_id", "genre_name", videogenre.videogenre_id_genre);
            ViewBag.videogenre_id_video = new SelectList(db.video, "video_id", "video_name", videogenre.videogenre_id_video);
            return View(videogenre);
        }

        // POST: videogenres/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "videogenre_id,videogenre_id_genre,videogenre_id_video")] videogenre videogenre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videogenre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.videogenre_id_genre = new SelectList(db.genre, "genre_id", "genre_name", videogenre.videogenre_id_genre);
            ViewBag.videogenre_id_video = new SelectList(db.video, "video_id", "video_name", videogenre.videogenre_id_video);
            return View(videogenre);
        }

        // GET: videogenres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            videogenre videogenre = db.videogenre.Find(id);
            if (videogenre == null)
            {
                return HttpNotFound();
            }
            return View(videogenre);
        }

        // POST: videogenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            videogenre videogenre = db.videogenre.Find(id);
            db.videogenre.Remove(videogenre);
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
