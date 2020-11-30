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
    public class ManagersController : Controller
    {
        private Entities db = new Entities();

        // GET: Managers
        public ActionResult Index()
        {
            var kierownicy = db.kierownicy.Include(k => k.dane_personalne);
            return View(kierownicy.ToList());
        }

        // GET: Managers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kierownicy kierownicy = db.kierownicy.Find(id);
            if (kierownicy == null)
            {
                return HttpNotFound();
            }
            return View(kierownicy);
        }

        // GET: Managers/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie");
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,wynagrodzenie")] kierownicy kierownicy)
        {
            if (ModelState.IsValid)
            {
                db.kierownicy.Add(kierownicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie", kierownicy.id);
            return View(kierownicy);
        }

        // GET: Managers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kierownicy kierownicy = db.kierownicy.Find(id);
            if (kierownicy == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie", kierownicy.id);
            return View(kierownicy);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,wynagrodzenie")] kierownicy kierownicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kierownicy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie", kierownicy.id);
            return View(kierownicy);
        }

        // GET: Managers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kierownicy kierownicy = db.kierownicy.Find(id);
            if (kierownicy == null)
            {
                return HttpNotFound();
            }
            return View(kierownicy);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kierownicy kierownicy = db.kierownicy.Find(id);
            db.kierownicy.Remove(kierownicy);
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
