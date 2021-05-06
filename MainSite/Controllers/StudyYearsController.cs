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
using MainSite.Utilities;

namespace MainSite.Controllers
{
    public class StudyYearsController : Controller
    {
        private TheDBContext db = new TheDBContext();

        // GET: StudyYears
        public ActionResult Index()
        {
            LoginCheck.Check(this);
            return View(db.StudyYears.ToList());
        }

        // GET: StudyYears/Details/5
        public ActionResult Details(int? id)
        {
            LoginCheck.Check(this);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyYear studyYear = db.StudyYears.Find(id);
            if (studyYear == null)
            {
                return HttpNotFound();
            }
            return View(studyYear);
        }

        // GET: StudyYears/Create
        public ActionResult Create()
        {
            LoginCheck.Check(this);
            return View();
        }

        // POST: StudyYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Year1,Year2")] StudyYear studyYear)
        {
            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                db.StudyYears.Add(studyYear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studyYear);
        }

        // GET: StudyYears/Edit/5
        public ActionResult Edit(int? id)
        {
            LoginCheck.Check(this);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyYear studyYear = db.StudyYears.Find(id);
            if (studyYear == null)
            {
                return HttpNotFound();
            }
            return View(studyYear);
        }

        // POST: StudyYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Year1,Year2")] StudyYear studyYear)
        {
            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                db.Entry(studyYear).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studyYear);
        }

        // GET: StudyYears/Delete/5
        public ActionResult Delete(int? id)
        {
            LoginCheck.Check(this);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyYear studyYear = db.StudyYears.Find(id);
            if (studyYear == null)
            {
                return HttpNotFound();
            }
            return View(studyYear);
        }

        // POST: StudyYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoginCheck.Check(this);
            StudyYear studyYear = db.StudyYears.Find(id);
            db.StudyYears.Remove(studyYear);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            LoginCheck.Check(this);
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
