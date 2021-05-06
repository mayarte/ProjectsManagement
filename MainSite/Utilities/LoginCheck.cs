using System.Collections.Generic;
using System.Web.Mvc;

namespace MainSite.Utilities
{
    public class LoginCheck
    {
        public static void Check(Controller controller, bool ignoreSession = false)
        {
            var path = ((System.Web.HttpRequestWrapper)controller.Request).AppRelativeCurrentExecutionFilePath;
            if (controller.Session["User"] == null)
            {
                if (path != "~/Home")
                {
                    if (!ignoreSession)
                    {
                        controller.Response.Redirect("~/Home");
                    }
                }
            }
            else
            {
                var myClearance = (List<string>)controller.Session["MyClearance"];
                if (myClearance.Contains(path.Split('/')[1]))
                {
                    controller.ViewBag.LoggeedUserName = ((Entity.MyData.SystemUser)controller.Session["User"]).Name;
                }
                else
                {
                    if (path != "~/Home")
                    {
                        if (!ignoreSession)
                        {
                            controller.Response.Redirect("~/Home");
                        }
                    }
                }
            }
        }

        public static Entity.MyData.SystemUser GetLoggedInUser(Controller controller)
        {
            if (controller.Session["User"] != null)
            {
                return (Entity.MyData.SystemUser)controller.Session["User"];
            }
            return null;
        }
    }
}