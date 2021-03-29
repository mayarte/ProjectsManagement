using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entity;
using Entity.MyData;

namespace MainSite.Controllers
{
    public class ClearancesController : Controller
    {
        private TheDBContext db = new TheDBContext();

        // GET: Clearances
        public ActionResult Index()
        {
            return View(db.Clearances.ToList());
        }

        // GET: Clearances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clearance clearance = db.Clearances.Find(id);
            if (clearance == null)
            {
                return HttpNotFound();
            }
            return View(clearance);
        }

        // GET: Clearances/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clearances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Clearance clearance)
        {
            if (ModelState.IsValid)
            {
                db.Clearances.Add(clearance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clearance);
        }

        // GET: Clearances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clearance clearance = db.Clearances.Find(id);
            if (clearance == null)
            {
                return HttpNotFound();
            }
            return View(clearance);
        }

        // POST: Clearances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Clearance clearance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clearance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clearance);
        }

        // GET: Clearances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clearance clearance = db.Clearances.Find(id);
            if (clearance == null)
            {
                return HttpNotFound();
            }
            return View(clearance);
        }

        // POST: Clearances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clearance clearance = db.Clearances.Find(id);
            db.Clearances.Remove(clearance);
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
