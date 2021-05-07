using Entity;
using MainSite.Models;
using MainSite.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace MainSite.Controllers
{
    public class ProposeProjectController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private TheDBContext db = new TheDBContext();

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
            ProposeProjectContent ProposeProject = new ProposeProjectContent();
            ProposeProject.Years = db.StudyYears.ToList();
            return View(ProposeProject);
        }

        // POST: ProposeProject/Create
        [HttpPost]
        public ActionResult Create(ProposeProjectContent ProposeProject)
        {
            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                try
                {
                    var project = new Entity.MyData.ProjectData();
                    project.Name = ProposeProject.ProjectData.Name;
                    project.Description = ProposeProject.ProjectData.Description;
                    project.Status = Entity.MyData.StateEnum.Active;
                    project.CreationDate = DateTime.Now;
                    project.ProgressPercentage = 0;
                    project.Year = db.StudyYears.FirstOrDefault(x => x.ID == ProposeProject.YearID);
                    project.ProjectStatus = Entity.MyData.ProjectStatusEnum.Free;
                    db.ProjectsData.Add(project);
                    var currentUser = LoginCheck.GetLoggedInUser(this);

                    var user = db.Users.FirstOrDefault(x => x.ID == currentUser.ID);
                    var link = new Entity.MyData.ProjectUserLink()
                    {
                        LinkType = Entity.MyData.ProjectLinkEnum.Proposing,
                        Project = project,
                        User = user
                    };
                    db.ProjectUserLinks.Add(link);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    ProposeProject.ErrorMsg = "حدث خطا في خزن البيانات";
                }
            }
            ProposeProject.Years = db.StudyYears.ToList();
            return View(ProposeProject);
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
