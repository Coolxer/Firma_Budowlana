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
    public class MaterialsController : Controller
    {
        private Entities db = new Entities();

        // GET: Materials
        public ActionResult Index()
        {
            var materialy = db.materialy.Include(m => m.magazyny).Include(m => m.zlecenia);
            return View(materialy.ToList());
        }

        // GET: Materials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            materialy materialy = db.materialy.Find(id);
            if (materialy == null)
            {
                return HttpNotFound();
            }
            return View(materialy);
        }

        // GET: Materials/Create
        public ActionResult Create()
        {
            ViewBag.dostepny_w = new SelectList(db.magazyny, "id", "nazwa");
            ViewBag.zarezerwowany_dla = new SelectList(db.zlecenia, "id", "etap");
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nazwa,ilosc,wartosc,zarezerwowany_dla,dostepny_w")] materialy materialy)
        {
            if (ModelState.IsValid)
            {
                db.materialy.Add(materialy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dostepny_w = new SelectList(db.magazyny, "id", "nazwa", materialy.dostepny_w);
            ViewBag.zarezerwowany_dla = new SelectList(db.zlecenia, "id", "etap", materialy.zarezerwowany_dla);
            return View(materialy);
        }

        // GET: Materials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            materialy materialy = db.materialy.Find(id);
            if (materialy == null)
            {
                return HttpNotFound();
            }
            ViewBag.dostepny_w = new SelectList(db.magazyny, "id", "nazwa", materialy.dostepny_w);
            ViewBag.zarezerwowany_dla = new SelectList(db.zlecenia, "id", "etap", materialy.zarezerwowany_dla);
            return View(materialy);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nazwa,ilosc,wartosc,zarezerwowany_dla,dostepny_w")] materialy materialy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materialy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dostepny_w = new SelectList(db.magazyny, "id", "nazwa", materialy.dostepny_w);
            ViewBag.zarezerwowany_dla = new SelectList(db.zlecenia, "id", "etap", materialy.zarezerwowany_dla);
            return View(materialy);
        }

        // GET: Materials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            materialy materialy = db.materialy.Find(id);
            if (materialy == null)
            {
                return HttpNotFound();
            }
            return View(materialy);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            materialy materialy = db.materialy.Find(id);
            db.materialy.Remove(materialy);
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
