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
    public class WarehousesController : Controller
    {
        private Entities db = new Entities();

        // GET: Warehouses
        public ActionResult Index()
        {
            return View(db.magazyny.ToList());
        }

        // GET: Warehouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            magazyny magazyny = db.magazyny.Find(id);
            if (magazyny == null)
            {
                return HttpNotFound();
            }
            return View(magazyny);
        }

        // GET: Warehouses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nazwa,adres,stan_wypelnienia")] magazyny magazyny)
        {
            if (ModelState.IsValid)
            {
                db.magazyny.Add(magazyny);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(magazyny);
        }

        // GET: Warehouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            magazyny magazyny = db.magazyny.Find(id);
            if (magazyny == null)
            {
                return HttpNotFound();
            }
            return View(magazyny);
        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nazwa,adres,stan_wypelnienia")] magazyny magazyny)
        {
            if (ModelState.IsValid)
            {
                db.Entry(magazyny).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(magazyny);
        }

        // GET: Warehouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            magazyny magazyny = db.magazyny.Find(id);
            if (magazyny == null)
            {
                return HttpNotFound();
            }
            return View(magazyny);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            magazyny magazyny = db.magazyny.Find(id);
            db.magazyny.Remove(magazyny);
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
