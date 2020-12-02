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
    public class AgreementsController : Controller
    {
        private Entities db = new Entities();

        // GET: Agreements
        public ActionResult Index()
        {
            var umowy = db.umowy.Include(u => u.zlecenia);
            return View(umowy.ToList());
        }

        // GET: Agreements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            umowy umowy = db.umowy.Find(id);
            if (umowy == null)
            {
                return HttpNotFound();
            }
            return View(umowy);
        }

        // GET: Agreements/Create
        public ActionResult Create()
        {
            ViewBag.nr_zlecenia = new SelectList(db.zlecenia, "id", "etap");
            return View();
        }

        // POST: Agreements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nr_zlecenia,typ,data_wystawienia")] umowy umowy)
        {
            if (ModelState.IsValid)
            {
                db.umowy.Add(umowy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nr_zlecenia = new SelectList(db.zlecenia, "id", "etap", umowy.nr_zlecenia);
            return View(umowy);
        }

        // GET: Agreements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            umowy umowy = db.umowy.Find(id);
            if (umowy == null)
            {
                return HttpNotFound();
            }
            ViewBag.nr_zlecenia = new SelectList(db.zlecenia, "id", "etap", umowy.nr_zlecenia);
            return View(umowy);
        }

        // POST: Agreements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nr_zlecenia,typ,data_wystawienia")] umowy umowy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(umowy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nr_zlecenia = new SelectList(db.zlecenia, "id", "etap", umowy.nr_zlecenia);
            return View(umowy);
        }

        // GET: Agreements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            umowy umowy = db.umowy.Find(id);
            if (umowy == null)
            {
                return HttpNotFound();
            }
            return View(umowy);
        }

        // POST: Agreements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            umowy umowy = db.umowy.Find(id);
            db.umowy.Remove(umowy);
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
