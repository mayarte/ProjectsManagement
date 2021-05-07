using System.Collections.Generic;

namespace Entity.MyData
{
   public class SystemUserGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Clearance> Clearances { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
