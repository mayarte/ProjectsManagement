using Entity.MyData;
using MainSite.DataOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainSite.Models
{
    public class PostContent 
    {
        public ProjectPost Post { get; set; }
      
        [AllowHtml]
        public string YouTubeLink { get; set; }
        [AllowHtml]
        public string WebLink { get; set; }

        public ProjectData Project { get; set; }
        public string ErrorMsg { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase FileUpload { get; set; }
    }
}