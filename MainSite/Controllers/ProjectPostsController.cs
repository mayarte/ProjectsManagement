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
    public class ProjectPostsController : Controller
    {
        private TheDBContext db = new TheDBContext();

        // GET: ProjectPosts
        public ActionResult Index()
        {
            LoginCheck.Check(this);
            return View(db.ProjectPosts.ToList());
        }

        // GET: ProjectPosts/Details/5
        public ActionResult Details(int? id)
        {
            LoginCheck.Check(this);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectPost projectPost = db.ProjectPosts.Find(id);
            if (projectPost == null)
            {
                return HttpNotFound();
            }
            return View(projectPost);
        }

        // GET: ProjectPosts/Create
        public ActionResult Create()
        {
            LoginCheck.Check(this);
            return View();
        }

        // POST: ProjectPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CreationDate,LastUpdateTime,Text,MoreInfo,PostType,Status")] ProjectPost projectPost)
        {
            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                db.ProjectPosts.Add(projectPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projectPost);
        }

        // GET: ProjectPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            LoginCheck.Check(this);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectPost projectPost = db.ProjectPosts.Find(id);
            if (projectPost == null)
            {
                return HttpNotFound();
            }
            return View(projectPost);
        }

        // POST: ProjectPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CreationDate,LastUpdateTime,Text,MoreInfo,PostType,Status")] ProjectPost projectPost)
        {
            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                db.Entry(projectPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectPost);
        }

        // GET: ProjectPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            LoginCheck.Check(this);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectPost projectPost = db.ProjectPosts.Find(id);
            if (projectPost == null)
            {
                return HttpNotFound();
            }
            return View(projectPost);
        }

        // POST: ProjectPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoginCheck.Check(this);
            ProjectPost projectPost = db.ProjectPosts.Find(id);
            db.ProjectPosts.Remove(projectPost);
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
