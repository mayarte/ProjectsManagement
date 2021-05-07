using Entity.MyData;
using MainSite.DataOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.Models
{
    public class ProjectListContent
    {
        public List<ProjectListItem> ProjectList { get; set; }
        public SystemUser CurrentUser { get; set; }
        public string ErrorMsg { get; set; }
    }
}