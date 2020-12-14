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
    public class PersonalDataController : Controller
    {
        private Entities db = new Entities();

        // GET: PersonalData
        public ActionResult Index()
        {
            var dane_personalne = db.dane_personalne.Include(d => d.kierownicy).Include(d => d.klienci).Include(d => d.pracownicy);
            return View(dane_personalne.ToList());
        }

        [HttpPost]
        public JsonResult Exists([Bind(Include = "id,imie,nazwisko,pesel,nr_telefonu,email")] dane_personalne dane_personalne)
        {
            if (TempData["pesel"] != null)
            {
                if (dane_personalne.pesel == TempData["pesel"].ToString())
                    return Json(true);
                else
                    return Json(this.db.dane_personalne.Where(d => d.pesel == dane_personalne.pesel).FirstOrDefault() == null);
            }
            else
                return Json(this.db.dane_personalne.Where(d => d.pesel == dane_personalne.pesel).FirstOrDefault() == null);
        }

        // GET: PersonalData/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.kierownicy, "id", "id");
            ViewBag.id = new SelectList(db.klienci, "id", "nazwa_firmy");
            ViewBag.id = new SelectList(db.pracownicy, "id", "id");
            return View();
        }

        // POST: PersonalData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,imie,nazwisko,pesel,nr_telefonu,email")] dane_personalne dane_personalne)
        {
            if (ModelState.IsValid)
            {
                db.dane_personalne.Add(dane_personalne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.kierownicy, "id", "id", dane_personalne.id);
            ViewBag.id = new SelectList(db.klienci, "id", "nazwa_firmy", dane_personalne.id);
            ViewBag.id = new SelectList(db.pracownicy, "id", "id", dane_personalne.id);
            return View(dane_personalne);
        }

        // GET: PersonalData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dane_personalne dane_personalne = db.dane_personalne.Find(id);
            if (dane_personalne == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.kierownicy, "id", "id", dane_personalne.id);
            ViewBag.id = new SelectList(db.klienci, "id", "nazwa_firmy", dane_personalne.id);
            ViewBag.id = new SelectList(db.pracownicy, "id", "id", dane_personalne.id);
            return View(dane_personalne);
        }

        // POST: PersonalData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,imie,nazwisko,pesel,nr_telefonu,email")] dane_personalne dane_personalne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dane_personalne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.kierownicy, "id", "id", dane_personalne.id);
            ViewBag.id = new SelectList(db.klienci, "id", "nazwa_firmy", dane_personalne.id);
            ViewBag.id = new SelectList(db.pracownicy, "id", "id", dane_personalne.id);
            return View(dane_personalne);
        }

        // GET: PersonalData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dane_personalne dane_personalne = db.dane_personalne.Find(id);
            if (dane_personalne == null)
            {
                return HttpNotFound();
            }
            return View(dane_personalne);
        }

        // POST: PersonalData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dane_personalne dane_personalne = db.dane_personalne.Find(id);
            db.dane_personalne.Remove(dane_personalne);
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
