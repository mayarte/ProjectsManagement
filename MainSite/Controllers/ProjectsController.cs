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
    public class ProjectsController : Controller
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private TheDBContext db = new TheDBContext();

        // GET: Projects
        public ActionResult Index()
        {
            Response.Redirect("~/Home");
            return View();
        }

        // GET: Projects/ProjectList
        public ActionResult ProjectList()
        {
            LoginCheck.Check(this);
            var ProjectList = new ProjectListContent();
            try
            {
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
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return View(ProjectList);
        }

        // GET: Projects/MyProjects
        public ActionResult MyProjects()
        {
            LoginCheck.Check(this);
            var ProjectList = new ProjectListContent();
            try
            {
                ProjectList.CurrentUser = LoginCheck.GetLoggedInUser(this);

                var myProjectsLinks = db.ProjectUserLinks
                        .Include(x => x.User)
                        .Include(x => x.Project)
                        .AsNoTracking()
                        .Where(x => x.User.ID == ProjectList.CurrentUser.ID &&
                        x.Project.Status == Entity.MyData.StateEnum.Active).ToList();

                var projectList = myProjectsLinks.Select(x => x.Project).ToList();
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
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return View(ProjectList);
        }

        //Projects/AddMeToProject
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
                        return RedirectToAction("Projects");
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
            return RedirectToAction("Projects");
        }

        //Projects/ShowProject
        public ActionResult ShowProject(int id)
        {
            LoginCheck.Check(this);
            var showProjectContent = new ShowProjectContent();
            showProjectContent.Project = db.ProjectsData.AsNoTracking().FirstOrDefault(x => x.Status == Entity.MyData.StateEnum.Active && x.ID == id);
            showProjectContent.CurrentUser = LoginCheck.GetLoggedInUser(this);
            showProjectContent.Links = db.ProjectUserLinks
                        .Include(x => x.Project)
                        .Include(x => x.User)
                        .AsNoTracking().Where(x => x.Project.ID == id).ToList();
            showProjectContent.Years = db.StudyYears.ToList();
            return View(showProjectContent);
        }

        // POST: Projects/ShowProject
        [HttpPost]
        public ActionResult ShowProject(ShowProjectContent content)
        {
            LoginCheck.Check(this);

            if (ModelState.IsValid)
            {
                try
                {
                    var project = db.ProjectsData.FirstOrDefault(x => x.ID == content.Project.ID);
                    if (project == null)
                    {
                        return View(content);
                    }
                    project.Name = content.Project.Name;
                    project.Description = content.Project.Description;
                    project.Status = content.Project.Status;
                    project.ProgressPercentage = content.Project.ProgressPercentage;
                    project.Year = db.StudyYears.FirstOrDefault(x => x.ID == content.YearID);
                    project.ProjectStatus = content.Project.ProjectStatus;

                    db.ProjectsData.AddOrUpdate(project);
                    db.SaveChanges();
                    return RedirectToAction("ShowProject", content.Project.ID);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    content.ErrorMsg = "حدث خطا في خزن البيانات";
                }
            }
            content.Years = db.StudyYears.ToList();
            return View(content);
        }

        //Projects/RemoveMyRelation
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
            return RedirectToAction("Projects");
        }
    }
}