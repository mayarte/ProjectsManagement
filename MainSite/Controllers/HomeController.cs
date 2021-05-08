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
            LoginCheck.Check(this, true);
            return View();
        }

        public ActionResult Contact()
        {
            LoginCheck.Check(this, true);
            return View();
        }
    }
}