using Entity.MyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.DataOnly
{
    public class ProjectListItem
    {
        public ProjectData Project { get; set; }
        public List<ProjectUserLink> Users { get; set; }
    }
}