using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.MyData
{
    public class StudyYear
    {
        public int ID { get; set; }
        public string Year1 { get; set; }
        public string Year2 { get; set; }
        public override string ToString()
        {
            return $"{Year1}/{Year2}";
        }
    }
}
