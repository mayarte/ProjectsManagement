using MainSite.Models;
using MainSite.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            LoginCheck.Check(this);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            LoginCheck.Check(this, true);
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            LoginCheck.Check(this, true);
            return View();
        }
    }
}