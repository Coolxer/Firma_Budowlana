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
    public class TasksController : Controller
    {
        private Entities db = new Entities();

        // GET: Tasks
        public ActionResult Index(bool checkbox = false)
        {
            TempData["checkbox"] = checkbox;

            if (checkbox)
            {
                var zlecenia = db.zlecenia.Include(z => z.kierownicy).Include(z => z.zgloszenia).Where(z => z.postep == 100);
                return View(zlecenia.ToList());
            }
            else
            {
                var zlecenia = db.zlecenia.Include(z => z.kierownicy).Include(z => z.zgloszenia);
                return View(zlecenia.ToList());
            }
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.kierownik = new SelectList(db.kierownicy, "id", "dane_personalne.fullName", db.zlecenia.Include(g => g.kierownicy));;
            ViewBag.nr_zgloszenia = new SelectList(db.zgloszenia, "id", "id");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nr_zgloszenia,kierownik,etap,postep,szacunkowy_koszt,termin")] zlecenia zlecenia)
        {
            if (ModelState.IsValid)
            {
                db.zlecenia.Add(zlecenia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.kierownik = new SelectList(db.kierownicy, "id", "dane_personalne.fullName", db.zlecenia.Include(g => g.kierownicy));
            ViewBag.nr_zgloszenia = new SelectList(db.zgloszenia, "id", "opis", zlecenia.nr_zgloszenia);
            return View(zlecenia);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zlecenia zlecenia = db.zlecenia.Find(id);
            if (zlecenia == null)
            {
                return HttpNotFound();
            }
            ViewBag.kierownik = new SelectList(db.kierownicy, "id", "dane_personalne.fullName", db.zlecenia.Include(g => g.kierownicy));
            ViewBag.nr_zgloszenia = new SelectList(db.zgloszenia, "id", "id", zlecenia.nr_zgloszenia);
            return View(zlecenia);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nr_zgloszenia,kierownik,etap,postep,szacunkowy_koszt,termin")] zlecenia zlecenia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zlecenia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kierownik = new SelectList(db.kierownicy, "id", "dane_personalne.fullName", db.zlecenia.Include(g => g.kierownicy));
            ViewBag.nr_zgloszenia = new SelectList(db.zgloszenia, "id", "opis", zlecenia.nr_zgloszenia);
            return View(zlecenia);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zlecenia zlecenia = db.zlecenia.Find(id);
            if (zlecenia == null)
            {
                return HttpNotFound();
            }
            return View(zlecenia);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            zlecenia zlecenia = db.zlecenia.Find(id);
            
            try
            {
                db.zlecenia.Remove(zlecenia);
                db.SaveChanges();
            }
            catch (DataException error)
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
