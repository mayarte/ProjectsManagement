using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.MyData
{
    public class ProjectUserLink
    {
        public int ID { get; set; }
        public SystemUser User { get; set; }
        public ProjectData Project { get; set; }
        public ProjectLinkEnum LinkType { get; set; }
    }
}
