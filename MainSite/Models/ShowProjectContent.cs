using Entity.MyData;
using MainSite.DataOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.Models
{
    public class ShowProjectContent
    {
        public ProjectData Project { get; set; }
        public List<ProjectPost> Posts { get; set; }
        public List<ProjectUserLink> Links { get; set; }
        public SystemUser CurrentUser { get; set; }
        public string ErrorMsg { get; set; }

        public int YearID { get; set; }
        public List<StudyYear> Years { get; set; }
    }
}