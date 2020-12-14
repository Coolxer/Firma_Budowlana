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
    public class MachinesController : Controller
    {
        private Entities db = new Entities();

        // GET: Machines
        public ActionResult Index(bool checkbox = false)
        {
            TempData["checkbox"] = checkbox;

            if (checkbox)
            {
                var maszyny = db.maszyny.Where(m => m.sprawna && !m.zajeta);
                return View(maszyny.ToList());
            }
            else
            {
                return View(db.maszyny.ToList());
            }
        }
        
        // GET: Machines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nazwa,sprawna,zajeta")] maszyny maszyny)
        {
            if (ModelState.IsValid)
            {
                db.maszyny.Add(maszyny);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maszyny);
        }

        // GET: Machines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            maszyny maszyny = db.maszyny.Find(id);
            if (maszyny == null)
            {
                return HttpNotFound();
            }
            return View(maszyny);
        }

        // POST: Machines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nazwa,sprawna,zajeta")] maszyny maszyny)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maszyny).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maszyny);
        }

        // GET: Machines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            maszyny maszyny = db.maszyny.Find(id);
            if (maszyny == null)
            {
                return HttpNotFound();
            }
            return View(maszyny);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            maszyny maszyny = db.maszyny.Find(id);
            
            try
            {
                db.maszyny.Remove(maszyny);
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
