using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.Models
{
    public class ProposeProjectContent
    {
        public Entity.MyData.ProjectData ProjectData { get; set; }
        public string ErrorMsg { get; set; }
    }
}