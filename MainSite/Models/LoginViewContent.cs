using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.Models
{
    public class LoginViewContent
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ErrorMsg { get; set; }
    }
}