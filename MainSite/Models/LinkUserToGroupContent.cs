using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity.MyData;
namespace MainSite.Models
{
    public class LinkUserToGroupContent
    {
        public SystemUser User { get; set; }
        public List<SystemUserGroup> Groups { get; set; }
        public int GroupID { get; set; }
    }
}