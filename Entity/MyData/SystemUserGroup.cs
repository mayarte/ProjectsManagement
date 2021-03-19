using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.MyData
{
   public class SystemUserGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Clearance> Clearances { get; set; }
    }
}
