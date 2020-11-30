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
    public class WorkersController : Controller
    {
        private Entities db = new Entities();

        // GET: Workers
        public ActionResult Index()
        {
            var pracownicy = db.pracownicy.Include(p => p.dane_personalne).Include(p => p.grupy_robocze).Include(p => p.maszyny);
            return View(pracownicy.ToList());
        }

        // GET: Workers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pracownicy pracownicy = db.pracownicy.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // GET: Workers/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie");
            ViewBag.grupa_robocza = new SelectList(db.grupy_robocze, "id", "specjalizacja");
            ViewBag.obslugiwana_maszyna = new SelectList(db.maszyny, "id", "nazwa");
            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,grupa_robocza,obslugiwana_maszyna,wynagrodzenie")] pracownicy pracownicy)
        {
            if (ModelState.IsValid)
            {
                db.pracownicy.Add(pracownicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie", pracownicy.id);
            ViewBag.grupa_robocza = new SelectList(db.grupy_robocze, "id", "specjalizacja", pracownicy.grupa_robocza);
            ViewBag.obslugiwana_maszyna = new SelectList(db.maszyny, "id", "nazwa", pracownicy.obslugiwana_maszyna);
            return View(pracownicy);
        }

        // GET: Workers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pracownicy pracownicy = db.pracownicy.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie", pracownicy.id);
            ViewBag.grupa_robocza = new SelectList(db.grupy_robocze, "id", "specjalizacja", pracownicy.grupa_robocza);
            ViewBag.obslugiwana_maszyna = new SelectList(db.maszyny, "id", "nazwa", pracownicy.obslugiwana_maszyna);
            return View(pracownicy);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,grupa_robocza,obslugiwana_maszyna,wynagrodzenie")] pracownicy pracownicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pracownicy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie", pracownicy.id);
            ViewBag.grupa_robocza = new SelectList(db.grupy_robocze, "id", "specjalizacja", pracownicy.grupa_robocza);
            ViewBag.obslugiwana_maszyna = new SelectList(db.maszyny, "id", "nazwa", pracownicy.obslugiwana_maszyna);
            return View(pracownicy);
        }

        // GET: Workers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pracownicy pracownicy = db.pracownicy.Find(id);
            if (pracownicy == null)
            {
                return HttpNotFound();
            }
            return View(pracownicy);
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pracownicy pracownicy = db.pracownicy.Find(id);
            db.pracownicy.Remove(pracownicy);
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
