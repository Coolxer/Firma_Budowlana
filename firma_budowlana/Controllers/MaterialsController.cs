﻿using System;
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
        public ActionResult Index(bool checkbox = false)
        {
            TempData["checkbox"] = checkbox;

            if (checkbox)
            {
                var materialy = db.materialy.Include(m => m.magazyny).Include(m => m.zlecenia).Where(m => m.dostepny_w != null);
                return View(materialy.ToList());
            }
            else
            {
                var materialy = db.materialy.Include(m => m.magazyny).Include(m => m.zlecenia);
                return View(materialy.ToList());
            }
        }

        // GET: Materials/Create
        public ActionResult Create()
        {
            ViewBag.zarezerwowany_dla = new SelectList(db.zlecenia, "id", "id", db.materialy.Include(z => z.zlecenia));
            ViewBag.dostepny_w = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = "0",
                    Text = "NIEDOSTEPNY",
                }

            }.Concat(db.magazyny.Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.nazwa,
            }));

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
                if (materialy.dostepny_w == 0) materialy.dostepny_w = null;
                db.materialy.Add(materialy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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

            ViewBag.zarezerwowany_dla = new SelectList(db.zlecenia, "id", "id", materialy.zarezerwowany_dla);

            ViewBag.dostepny_w = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = "0",
                    Text = "NIEDOSTEPNY",
                    Selected = materialy.dostepny_w == 0 ? true : false
                }

            }.Concat(db.magazyny.Select(x => new SelectListItem()
            {
                Value = x.id.ToString(),
                Text = x.nazwa,
                Selected = x.id == materialy.dostepny_w
            }));


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
                if (materialy.dostepny_w == 0) materialy.dostepny_w = null;
                db.Entry(materialy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
