using System;

namespace Entity.MyData
{
    public class ProjectData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StudyYear Year { get; set; }
        public DateTime? CreationDate { get; set; }
        public int ProgressPercentage { get; set; }
        public ProjectStatusEnum ProjectStatus { get; set; }
        public StateEnum Status { get; set; }
    }
}
