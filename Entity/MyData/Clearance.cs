using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.MyData
{
   public class Clearance
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<SystemUserGroup> Groups { get; set; }
    }
}
