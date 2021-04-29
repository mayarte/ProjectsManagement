using MainSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MainSite.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Login");
        }

        // GET: Login/Login
        public ActionResult Login()
        {
            return View((LoginViewContent)Session["LoginViewContent"]);
        }

        // POST: Login/Login
        [HttpPost]
        public ActionResult Login(LoginViewContent Content)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(Content.UserName))
                {
                    Content.ErrorMsg = "الرجاء ادخال اسم المستخدم";

                    Session["LoginViewContent"] = Content;
                    return RedirectToAction("Login");
                }

                if (string.IsNullOrWhiteSpace(Content.Password))
                {
                    Content.ErrorMsg = "الرجاء ادخال كلمة المرور";

                    Session["LoginViewContent"] = Content;
                    return RedirectToAction("Login");
                }

                using (Entity.TheDBContext context = new Entity.TheDBContext())
                {
                    var user = context.Users
                        .Include(x => x.ClearanceGroup)
                        .Include(x => x.ClearanceGroup.Clearances)
                        .FirstOrDefault(x => x.Name == Content.UserName && x.Password == Content.Password);

                    if (user == null)
                    {
                        Content.ErrorMsg = "خطا في ادخال البيانات او كلمة المرور غير صحيحة";

                        Session["LoginViewContent"] = Content;
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        Session["User"] = user;
                        Session["MyClearance"] = user.ClearanceGroup.Clearances.Select(x => x.Name).ToList();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(Content);
        }


        public ActionResult Logout()
        {
            Session["User"] = null;
            Session["MyClearance"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}