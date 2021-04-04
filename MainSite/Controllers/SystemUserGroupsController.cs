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
    public class SystemUserGroupsController : Controller
    {
        private TheDBContext db = new TheDBContext();

        // GET: SystemUserGroups
        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        // GET: SystemUserGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemUserGroup systemUserGroup = db.Groups.Find(id);
            if (systemUserGroup == null)
            {
                return HttpNotFound();
            }
            return View(systemUserGroup);
        }

        // GET: SystemUserGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SystemUserGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] SystemUserGroup systemUserGroup)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(systemUserGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systemUserGroup);
        }

        // GET: SystemUserGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemUserGroup systemUserGroup = db.Groups.Find(id);
            if (systemUserGroup == null)
            {
                return HttpNotFound();
            }
            return View(systemUserGroup);
        }

        // POST: SystemUserGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] SystemUserGroup systemUserGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(systemUserGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(systemUserGroup);
        }

        // GET: SystemUserGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemUserGroup systemUserGroup = db.Groups.Find(id);
            if (systemUserGroup == null)
            {
                return HttpNotFound();
            }
            return View(systemUserGroup);
        }

        // POST: SystemUserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SystemUserGroup systemUserGroup = db.Groups.Find(id);
            db.Groups.Remove(systemUserGroup);
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
