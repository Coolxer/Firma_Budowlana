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
    public class CustomersController : Controller
    {
        private Entities db = new Entities();

        // GET: Customers
        public ActionResult Index(bool checkbox = false)
        {
            TempData["checkbox"] = checkbox;

            if(checkbox)
            {
                var klienci = db.klienci.Include(k => k.dane_personalne).Where(k => k.nazwa_firmy != null);
                return View(klienci.ToList());
            }
            else
            {
                var klienci = db.klienci.Include(k => k.dane_personalne);
                return View(klienci.ToList());
            }
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nazwa_firmy,nip,dane_personalne")] klienci klienci)
        {
            if (ModelState.IsValid)
            {
                db.klienci.Add(klienci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie", klienci.id);
            return View(klienci);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            klienci klienci = db.klienci.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie", klienci.id);

            TempData["pesel"] = klienci.dane_personalne.pesel;

            return View(klienci);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nazwa_firmy,nip,dane_personalne")] klienci klienci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klienci).State = EntityState.Modified;
                db.Entry(klienci.dane_personalne).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie", klienci.id);
            return View(klienci);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            klienci klienci = db.klienci.Find(id);
            if (klienci == null)
            {
                return HttpNotFound();
            }
            return View(klienci);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            klienci klienci = db.klienci.Find(id);

            try
            {
                db.klienci.Remove(klienci);
                db.dane_personalne.Remove(db.dane_personalne.Find(id));
                db.SaveChanges();
            }
            catch(DataException error)
            {
                TempData["error"] = true;
                return RedirectToAction("Delete");
            }
           
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
