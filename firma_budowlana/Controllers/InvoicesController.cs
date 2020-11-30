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
    public class InvoicesController : Controller
    {
        private Entities db = new Entities();

        // GET: Invoices
        public ActionResult Index()
        {
            var faktury = db.faktury.Include(f => f.zlecenia);
            return View(faktury.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            faktury faktury = db.faktury.Find(id);
            if (faktury == null)
            {
                return HttpNotFound();
            }
            return View(faktury);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.nr_zlecenia = new SelectList(db.zlecenia, "id", "etap");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nr_zlecenia,typ,rodzaj_platnosci,termin_platnosci,data_wystawienia")] faktury faktury)
        {
            if (ModelState.IsValid)
            {
                db.faktury.Add(faktury);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nr_zlecenia = new SelectList(db.zlecenia, "id", "etap", faktury.nr_zlecenia);
            return View(faktury);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            faktury faktury = db.faktury.Find(id);
            if (faktury == null)
            {
                return HttpNotFound();
            }
            ViewBag.nr_zlecenia = new SelectList(db.zlecenia, "id", "etap", faktury.nr_zlecenia);
            return View(faktury);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nr_zlecenia,typ,rodzaj_platnosci,termin_platnosci,data_wystawienia")] faktury faktury)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faktury).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nr_zlecenia = new SelectList(db.zlecenia, "id", "etap", faktury.nr_zlecenia);
            return View(faktury);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            faktury faktury = db.faktury.Find(id);
            if (faktury == null)
            {
                return HttpNotFound();
            }
            return View(faktury);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            faktury faktury = db.faktury.Find(id);
            db.faktury.Remove(faktury);
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
