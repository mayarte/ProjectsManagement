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
using System.IO;

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
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Project.ID == id
                        && x.User.ID == currentUser.ID
                        && x.LinkType == Entity.MyData.ProjectLinkEnum.Working);
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

        //Projects/ShowProject
        public ActionResult ShowProject(int id)
        {
            LoginCheck.Check(this);

            var showProjectContent = new ShowProjectContent();
            showProjectContent.Project = db.ProjectsData.AsNoTracking()
                .Include(x => x.Year)
                .FirstOrDefault(x => x.Status == Entity.MyData.StateEnum.Active && x.ID == id);
            showProjectContent.CurrentUser = LoginCheck.GetLoggedInUser(this);
            showProjectContent.Links = db.ProjectUserLinks
                        .Include(x => x.Project)
                        .Include(x => x.User)
                        .AsNoTracking().Where(x => x.Project.ID == id).ToList();
            showProjectContent.Years = db.StudyYears.ToList();
            showProjectContent.YearID = showProjectContent.Project.Year != null ? showProjectContent.Project.Year.ID : 0;
            showProjectContent.Posts = db.ProjectPosts.AsNoTracking()
                .Include(x => x.Project)
                .Include(x => x.PostedBy)
                .Where(x => x.Project.ID == id && x.Status == Entity.MyData.StateEnum.Active).ToList();
            showProjectContent.UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"];
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
                    return RedirectToAction("ShowProject", "Projects", new { id = content.Project.ID });
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    content.ErrorMsg = "حدث خطا في خزن البيانات";
                }
            }
            content.YearID = content.Project.Year != null ? content.Project.Year.ID : 0;
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
            return RedirectToAction("ProjectList");
        }

        // GET: Projects/AddPost
        public ActionResult AddPost(int id)
        {
            LoginCheck.Check(this);
            var post = new PostContent();
            post.Post = new Entity.MyData.ProjectPost();
            post.Project = db.ProjectsData.AsNoTracking().FirstOrDefault(x => x.ID == id);
            return View(post);
        }

        // POST: Projects/AddPost
        [HttpPost]
        public ActionResult AddPost(PostContent postContent)
        {
            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                try
                {
                    var UploadedData = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadPath"]);
                    switch (postContent.Post.PostType)
                    {
                        case Entity.MyData.PostTypesEnum.Select_Type:
                            postContent.ErrorMsg = "الرجاء اختيار نوع المنشور";
                            return View(postContent);
                        case Entity.MyData.PostTypesEnum.Image:
                            if (postContent.ImageUpload != null && postContent.ImageUpload.ContentLength > 0)
                            {
                                var name = Guid.NewGuid().ToString() + Path.GetExtension(postContent.ImageUpload.FileName);
                                var dir = new DirectoryInfo(UploadedData);
                                if (!dir.Exists) dir.Create();
                                string path = Path.Combine(dir.FullName, name);
                                postContent.ImageUpload.SaveAs(path);
                                postContent.Post.Text = name;
                            }
                            break;
                        case Entity.MyData.PostTypesEnum.File:
                            if (postContent.FileUpload != null && postContent.FileUpload.ContentLength > 0)
                            {
                                var name = Guid.NewGuid().ToString() + Path.GetExtension(postContent.FileUpload.FileName);
                                var dir = new DirectoryInfo(UploadedData);
                                if (!dir.Exists) dir.Create();
                                string path = Path.Combine(dir.FullName, name);
                                postContent.FileUpload.SaveAs(path);
                                postContent.Post.Text = name;
                            }
                            break;
                        case Entity.MyData.PostTypesEnum.Text:
                            break;
                        case Entity.MyData.PostTypesEnum.WebLink:
                            postContent.Post.Text = postContent.WebLink;
                            break;
                        case Entity.MyData.PostTypesEnum.YouTubeLink:
                            postContent.Post.Text = postContent.YouTubeLink;
                            break;
                    }
                    var CurrentUser = LoginCheck.GetLoggedInUser(this);

                    postContent.Post.PostedBy = db.Users.FirstOrDefault(x => x.ID == CurrentUser.ID);
                    postContent.Post.Project = db.ProjectsData.FirstOrDefault(x => x.ID == postContent.Project.ID);
                    postContent.Post.LastUpdateTime = DateTime.Now;
                    postContent.Post.CreationDate = DateTime.Now;

                    db.ProjectPosts.Add(postContent.Post);
                    db.SaveChanges();
                    return RedirectToAction("ShowProject", "Projects", new { id = postContent.Post.Project.ID });
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    postContent.ErrorMsg = "حدث خطا في خزن البيانات";
                }
            }

            return View(postContent);
        }


        //Projects/DeletePost
        public ActionResult DeletePost(int id)
        {
            var pst = db.ProjectPosts
                     .Include(x => x.PostedBy)
                     .Include(x => x.Project)
                     .FirstOrDefault(x => x.ID == id);

            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = LoginCheck.GetLoggedInUser(this);
                    var user = db.Users.FirstOrDefault(x => x.ID == currentUser.ID);

                    if (pst.PostedBy.ID != user.ID)
                    {
                        return RedirectToAction("ShowProject", "Projects", new { id = pst.Project.ID });
                    }
                    pst.Status = Entity.MyData.StateEnum.Deleted;
                    pst.LastUpdateTime = DateTime.Now;
                    db.ProjectPosts.AddOrUpdate(pst);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return RedirectToAction("ShowProject", "Projects", new { id = pst.Project.ID });
        }


        // GET: Projects/EditPost
        public ActionResult EditPost(int id)
        {
            LoginCheck.Check(this);
            var content = new PostContent();
            var post = db.ProjectPosts.AsNoTracking()
                .Include(x => x.PostedBy)
                .Include(x => x.Project)
                .FirstOrDefault(x => x.ID == id);
            if (post == null)
            {
                return RedirectToAction("ProjectList");
            }
            content.Post = post;
            content.Project = post.Project;
            switch (content.Post.PostType)
            {
                case Entity.MyData.PostTypesEnum.Select_Type:
                    break;
                case Entity.MyData.PostTypesEnum.Text:
                    break;
                case Entity.MyData.PostTypesEnum.Image:
                    break;
                case Entity.MyData.PostTypesEnum.YouTubeLink:
                    content.YouTubeLink = content.Post.Text;
                    break;
                case Entity.MyData.PostTypesEnum.WebLink:
                    content.WebLink = content.Post.Text;
                    break;
                case Entity.MyData.PostTypesEnum.File:
                    break;
                default:
                    break;
            }
            return View(content);
        }

        // POST: Projects/EditPost
        [HttpPost]
        public ActionResult EditPost(PostContent postContent)
        {
            LoginCheck.Check(this);
            if (ModelState.IsValid)
            {
                try
                {
                    var UploadedData = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadPath"]);

                    var pst = db.ProjectPosts
                               .Include(x => x.PostedBy)
                               .Include(x => x.Project)
                               .FirstOrDefault(x => x.ID == postContent.Post.ID);

                    switch (postContent.Post.PostType)
                    {
                        case Entity.MyData.PostTypesEnum.Select_Type:
                            postContent.ErrorMsg = "الرجاء اختيار نوع المنشور";
                            return View(postContent);
                        case Entity.MyData.PostTypesEnum.Image:
                            if (postContent.ImageUpload != null && postContent.ImageUpload.ContentLength > 0)
                            {
                                var name = Guid.NewGuid().ToString() + Path.GetExtension(postContent.ImageUpload.FileName);
                                var dir = new DirectoryInfo(UploadedData);
                                if (!dir.Exists) dir.Create();
                                string path = Path.Combine(dir.FullName, name);
                                postContent.ImageUpload.SaveAs(path);
                                pst.Text = name;
                            }
                            break;
                        case Entity.MyData.PostTypesEnum.File:
                            if (postContent.FileUpload != null && postContent.FileUpload.ContentLength > 0)
                            {
                                var name = Guid.NewGuid().ToString() + Path.GetExtension(postContent.FileUpload.FileName);
                                var dir = new DirectoryInfo(UploadedData);
                                if (!dir.Exists) dir.Create();
                                string path = Path.Combine(dir.FullName, name);
                                postContent.FileUpload.SaveAs(path);
                                pst.Text = name;
                            }
                            break;
                        case Entity.MyData.PostTypesEnum.Text:
                            pst.Text = postContent.Post.Text;
                            break;
                        case Entity.MyData.PostTypesEnum.WebLink:
                            pst.Text = postContent.WebLink;
                            break;
                        case Entity.MyData.PostTypesEnum.YouTubeLink:
                            pst.Text = postContent.YouTubeLink;
                            break;
                    }
                    pst.PostType = postContent.Post.PostType;

                    var CurrentUser = LoginCheck.GetLoggedInUser(this);

                    pst.PostedBy = db.Users.FirstOrDefault(x => x.ID == CurrentUser.ID);
                    pst.Project = db.ProjectsData.FirstOrDefault(x => x.ID == postContent.Project.ID);
                    pst.LastUpdateTime = DateTime.Now;
                    pst.MoreInfo = postContent.Post.MoreInfo;

                    db.ProjectPosts.AddOrUpdate(pst);
                    db.SaveChanges();
                    return RedirectToAction("ShowProject", "Projects", new { id = pst.Project.ID });
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    postContent.ErrorMsg = "حدث خطا في خزن البيانات";
                }
            }

            return View(postContent);
        }
    }
}