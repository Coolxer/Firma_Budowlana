using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using firma_budowlana.Models;

namespace firma_budowlana.Controllers
{
    public class NotificationsController : Controller
    {
        private Entities db = new Entities();

        // GET: Notifications
        public ActionResult Index()
        {
            var zgloszenia = db.zgloszenia.Include(z => z.klienci);
            return View(zgloszenia.ToList());
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            ViewBag.autor_zgloszenia = new SelectList(db.klienci, "id", "dane_personalne.fullName", db.zgloszenia.Include(g => g.klienci));
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,autor_zgloszenia,opis,data_utworzenia")] zgloszenia zgloszenia)
        {
            if (ModelState.IsValid)
            {
                db.zgloszenia.Add(zgloszenia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.autor_zgloszenia = new SelectList(db.klienci, "id", "dane_personalne.fullName", db.zgloszenia.Include(g => g.klienci));
            return View(zgloszenia);
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zgloszenia zgloszenia = db.zgloszenia.Find(id);
            if (zgloszenia == null)
            {
                return HttpNotFound();
            }
            ViewBag.autor_zgloszenia = new SelectList(db.klienci, "id", "dane_personalne.fullName", db.zgloszenia.Include(g => g.klienci));
            return View(zgloszenia);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,autor_zgloszenia,opis,data_utworzenia")] zgloszenia zgloszenia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zgloszenia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.autor_zgloszenia = new SelectList(db.klienci, "id", "dane_personalne.fullName", db.zgloszenia.Include(g => g.klienci));
            return View(zgloszenia);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zgloszenia zgloszenia = db.zgloszenia.Find(id);
            if (zgloszenia == null)
            {
                return HttpNotFound();
            }
            return View(zgloszenia);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            zgloszenia zgloszenia = db.zgloszenia.Find(id);
            db.zgloszenia.Remove(zgloszenia);
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
