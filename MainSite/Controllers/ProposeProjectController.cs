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

        // GET: ProposeProject/ProjectList
        public ActionResult ProjectList()
        {
            LoginCheck.Check(this);
            ProjectListContent ProjectList = new ProjectListContent();
            var projectList = db.ProjectsData.AsNoTracking().Where(x => x.Status == Entity.MyData.StateEnum.Active).ToList();
            ProjectList.CurrentUser = LoginCheck.GetLoggedInUser(this);
            ProjectList.ProjectList = new List<DataOnly.ProjectListItem>();
            foreach (var item in projectList)
            {
                var data = new DataOnly.ProjectListItem();
                data.Project = item;
                data.Users = db.ProjectUserLinks
                    .Include(x => x.User)
                    .AsNoTracking()
                    .Where(x => x.Project.ID == item.ID).ToList();
                ProjectList.ProjectList.Add(data);
            }
            return View(ProjectList);
        }

        //ProposeProject/AddMeToProject
        public ActionResult AddMeToProject(int id)
        {
            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = LoginCheck.GetLoggedInUser(this);
                    var user = db.Users.FirstOrDefault(x => x.ID == currentUser.ID);
                    var project = db.ProjectsData.FirstOrDefault(x => x.ID == id);
                    
                    var dbLink = db.ProjectUserLinks
                        .Include(x => x.Project)
                        .Include(x => x.User)
                        .AsNoTracking().FirstOrDefault(x => x.Project.ID == id && x.User.ID == currentUser.ID && x.LinkType == Entity.MyData.ProjectLinkEnum.Working);
                    if (dbLink != null)
                    {
                        return RedirectToAction("ProjectList");
                    }

                    var link = new Entity.MyData.ProjectUserLink()
                    {
                        LinkType = Entity.MyData.ProjectLinkEnum.Working,
                        Project = project,
                        User = user
                    };
                    db.ProjectUserLinks.Add(link);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return RedirectToAction("ProjectList");
        }

        //ProposeProject/RemoveMyRelation
        public ActionResult RemoveMyRelation(int id)
        {
            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                try
                {
                    var link = db.ProjectUserLinks.FirstOrDefault(x => x.ID == id);
                    db.ProjectUserLinks.Remove(link);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return RedirectToAction("ProjectList");
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
