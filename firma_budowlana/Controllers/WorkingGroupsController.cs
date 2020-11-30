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
    public class WorkingGroupsController : Controller
    {
        private Entities db = new Entities();

        // GET: WorkingGroups
        public ActionResult Index()
        {
            var grupy_robocze = db.grupy_robocze.Include(g => g.kierownicy);
            return View(grupy_robocze.ToList());
        }

        // GET: WorkingGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grupy_robocze grupy_robocze = db.grupy_robocze.Find(id);
            if (grupy_robocze == null)
            {
                return HttpNotFound();
            }
            return View(grupy_robocze);
        }

        // GET: WorkingGroups/Create
        public ActionResult Create()
        {
            ViewBag.kierownik = new SelectList(db.kierownicy, "id", "id");
            return View();
        }

        // POST: WorkingGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,kierownik,specjalizacja")] grupy_robocze grupy_robocze)
        {
            if (ModelState.IsValid)
            {
                db.grupy_robocze.Add(grupy_robocze);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.kierownik = new SelectList(db.kierownicy, "id", "id", grupy_robocze.kierownik);
            return View(grupy_robocze);
        }

        // GET: WorkingGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grupy_robocze grupy_robocze = db.grupy_robocze.Find(id);
            if (grupy_robocze == null)
            {
                return HttpNotFound();
            }
            ViewBag.kierownik = new SelectList(db.kierownicy, "id", "id", grupy_robocze.kierownik);
            return View(grupy_robocze);
        }

        // POST: WorkingGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,kierownik,specjalizacja")] grupy_robocze grupy_robocze)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupy_robocze).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kierownik = new SelectList(db.kierownicy, "id", "id", grupy_robocze.kierownik);
            return View(grupy_robocze);
        }

        // GET: WorkingGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grupy_robocze grupy_robocze = db.grupy_robocze.Find(id);
            if (grupy_robocze == null)
            {
                return HttpNotFound();
            }
            return View(grupy_robocze);
        }

        // POST: WorkingGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            grupy_robocze grupy_robocze = db.grupy_robocze.Find(id);
            db.grupy_robocze.Remove(grupy_robocze);
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
