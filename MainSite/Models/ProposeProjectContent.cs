﻿using Entity.MyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainSite.Models
{
    public class ProposeProjectContent
    {
        public ProjectData ProjectData { get; set; }
        public string ErrorMsg { get; set; }
        public int YearID { get; set; }
        public List<StudyYear> Years { get; set; }
    }
}