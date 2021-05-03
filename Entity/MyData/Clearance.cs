using System.Collections.Generic;

namespace Entity.MyData
{
    public class Clearance
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<SystemUserGroup> Groups { get; set; }
    }
}
