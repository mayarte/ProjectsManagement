using MainSite.Models;
using MainSite.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainSite.Controllers
{
    public class ProposeProjectController : Controller
    {
        // GET: ProposeProject
        public ActionResult Index()
        {
            Response.Redirect("~/Home");
            return View();
        }

        // GET: ProposeProject/Details/5
        public ActionResult Details(int id)
        {
            LoginCheck.Check(this);
            return View();
        }

        // GET: ProposeProject/Create
        public ActionResult Create()
        {
            LoginCheck.Check(this);
            return View();
        }

        // POST: ProposeProject/Create
        [HttpPost]
        public ActionResult Create(ProposeProjectContent ProposeProject)
        {
            LoginCheck.Check(this);
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProposeProject/Edit/5
        public ActionResult Edit(int id)
        {
            LoginCheck.Check(this);
            return View();
        }

        // POST: ProposeProject/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProposeProjectContent ProposeProject)
        {
            LoginCheck.Check(this);
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProposeProject/Delete/5
        public ActionResult Delete(int id)
        {
            LoginCheck.Check(this);
            return View();
        }

        // POST: ProposeProject/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProposeProjectContent ProposeProject)
        {
            LoginCheck.Check(this);
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
