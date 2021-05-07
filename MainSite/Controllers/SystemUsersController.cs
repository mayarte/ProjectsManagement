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
using MainSite.Models;
using MainSite.Utilities;

namespace MainSite.Controllers
{
    public class SystemUsersController : Controller
    {
        private TheDBContext db = new TheDBContext();

        // GET: SystemUsers
        public ActionResult Index()
        {
            LoginCheck.Check(this);
            return View(db.Users.ToList());
        }

        // GET: SystemUsers/Details/5
        public ActionResult Details(int? id)
        {
            LoginCheck.Check(this);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemUser systemUser = db.Users.Find(id);
            if (systemUser == null)
            {
                return HttpNotFound();
            }
            return View(systemUser);
        }

        // GET: SystemUsers/Create
        public ActionResult Create()
        {
            LoginCheck.Check(this);
            return View();
        }

        // POST: SystemUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,Phone,Password")] SystemUser systemUser)
        {
            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                db.Users.Add(systemUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systemUser);
        }

        // GET: SystemUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            LoginCheck.Check(this);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemUser systemUser = db.Users.Find(id);
            if (systemUser == null)
            {
                return HttpNotFound();
            }
            return View(systemUser);
        }

        // POST: SystemUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,Phone,Password")] SystemUser systemUser)
        {
            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                db.Entry(systemUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(systemUser);
        }

        // GET: SystemUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            LoginCheck.Check(this);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemUser systemUser = db.Users.Find(id);
            if (systemUser == null)
            {
                return HttpNotFound();
            }
            return View(systemUser);
        }

        // POST: SystemUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoginCheck.Check(this);
            SystemUser systemUser = db.Users.Find(id);
            db.Users.Remove(systemUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: SystemUsers/Edit/5
        public ActionResult AddToGroup(int? id)
        {
            LoginCheck.Check(this);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var content = new LinkUserToGroupContent();

            content.User = db.Users.AsNoTracking()
                .Include(x => x.ClearanceGroup).FirstOrDefault(x => x.ID == id);

            if (content.User == null)
            {
                return HttpNotFound();
            }
            if(content.User.ClearanceGroup!=null)
            {
                content.GroupID = content.User.ClearanceGroup.ID;
            }
            content.Groups = db.Groups.AsNoTracking().ToList();

            return View(content);
        }

        // POST: SystemUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToGroup(LinkUserToGroupContent content)
        {
            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                var user = db.Users
                    .Include(x => x.ClearanceGroup)
                    .FirstOrDefault(x => x.ID == content.User.ID);

                var group = db.Groups
                   .FirstOrDefault(x => x.ID == content.GroupID);
                user.ClearanceGroup = group;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            content.Groups = db.Groups.AsNoTracking().ToList();
            return View(content);
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
