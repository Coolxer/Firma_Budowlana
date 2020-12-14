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
    public class AgreementsController : Controller
    {
        private Entities db = new Entities();

        // GET: Agreements
        public ActionResult Index()
        {
            var umowy = db.umowy.Include(u => u.zlecenia);
            return View(umowy.ToList());
        }

        // GET: Agreements/Create
        public ActionResult Create()
        {
            ViewBag.nr_zlecenia = new SelectList(this.db.zlecenia.Select(z => z.id).Except(this.db.umowy.Select(zl => zl.nr_zlecenia).ToList()));
            return View();
        }

        // POST: Agreements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nr_zlecenia,typ,data_wystawienia")] umowy umowy)
        {
            if (ModelState.IsValid)
            {
                db.umowy.Add(umowy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(umowy);
        }

        // GET: Agreements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            umowy umowy = db.umowy.Find(id);
            if (umowy == null)
            {
                return HttpNotFound();
            }
            ViewBag.nr_zlecenia = new SelectList(this.db.zlecenia.Select(z => z.id).Except(this.db.umowy.Where(z => z.nr_zlecenia != id).Select(z => z.nr_zlecenia)).ToList());
            return View(umowy);
        }

        // POST: Agreements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nr_zlecenia,typ,data_wystawienia")] umowy umowy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(umowy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(umowy);
        }

        // GET: Agreements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            umowy umowy = db.umowy.Find(id);
            if (umowy == null)
            {
                return HttpNotFound();
            }
            return View(umowy);
        }

        // POST: Agreements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            umowy umowy = db.umowy.Find(id);
            
            try
            {
                db.umowy.Remove(umowy);
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
