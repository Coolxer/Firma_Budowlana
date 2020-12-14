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
        public ActionResult Index(bool checkbox = false)
        {
            TempData["checkbox"] = checkbox;

            if (checkbox)
            {
                var pracownicy = db.pracownicy.Include(p => p.dane_personalne).Include(p => p.grupy_robocze).Include(p => p.maszyny).Where(p => p.obslugiwana_maszyna != null);
                return View(pracownicy.ToList());
            }
            else
            {
                var pracownicy = db.pracownicy.Include(p => p.dane_personalne).Include(p => p.grupy_robocze).Include(p => p.maszyny);
                return View(pracownicy.ToList());
            }
        }

        // GET: Workers/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie");
            ViewBag.grupa_robocza = new SelectList(db.grupy_robocze, "id", "specjalizacja");

            ViewBag.obslugiwana_maszyna = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = "0",
                    Text = "BRAK MASZYNY",
                }

            }.Concat(db.maszyny.Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.nazwa,
                Disabled = !x.sprawna || x.zajeta
            }));

            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,grupa_robocza,obslugiwana_maszyna,wynagrodzenie,dane_personalne")] pracownicy pracownicy)
        {
            if (ModelState.IsValid)
            {
                db.pracownicy.Add(pracownicy);

                var machine = pracownicy.obslugiwana_maszyna != 0 ? db.maszyny.Find(pracownicy.obslugiwana_maszyna) : null;

                if (machine != null)
                {
                    machine.zajeta = true;
                    db.Entry(machine).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id = new SelectList(db.dane_personalne, "id", "imie", pracownicy.id);
            ViewBag.grupa_robocza = new SelectList(db.grupy_robocze, "id", "specjalizacja");
            ViewBag.obslugiwana_maszyna = new SelectList(db.maszyny.Where(m => m.sprawna && !m.zajeta).ToList(), "id", "nazwa");
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

            TempData["lastMachine"] = pracownicy.obslugiwana_maszyna;

            ViewBag.obslugiwana_maszyna = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = "0",
                    Text = "BRAK MASZYNY",
                    Selected = pracownicy.obslugiwana_maszyna == 0 ? true : false
                }

            }.Concat(db.maszyny.Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.nazwa,
                Selected = x.id == pracownicy.obslugiwana_maszyna,
                Disabled = !x.sprawna || (x.zajeta && x.id != pracownicy.obslugiwana_maszyna)
            }));

            TempData["pesel"] = pracownicy.dane_personalne.pesel;

            //ViewBag.obslugiwana_maszyna = new SelectList(db.maszyny.Where(m => m.sprawna && !m.zajeta), "id", "nazwa");

            return View(pracownicy);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,grupa_robocza,obslugiwana_maszyna,wynagrodzenie,dane_personalne")] pracownicy pracownicy)
        {
            if (ModelState.IsValid)
            {
                var machine = TempData["lastMachine"] != null ? db.maszyny.Find(TempData["lastMachine"]) : null;

                if (pracownicy.obslugiwana_maszyna == 0 && machine != null)
                {
                    machine.zajeta = false;
                    pracownicy.obslugiwana_maszyna = null;
                }
                else if (pracownicy.obslugiwana_maszyna != 0)
                    db.maszyny.Find(pracownicy.obslugiwana_maszyna).zajeta = true;
                else
                    pracownicy.obslugiwana_maszyna = null;

                db.Entry(pracownicy).State = EntityState.Modified;

                if(machine != null)
                    db.Entry(machine).State = EntityState.Modified;

                db.Entry(pracownicy.dane_personalne).State = EntityState.Modified;
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
            db.dane_personalne.Remove(db.dane_personalne.Find(id));
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
