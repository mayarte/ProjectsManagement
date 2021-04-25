using System;

namespace Entity.MyData
{
  public  class ProjectPost
    {
        public int ID { get; set; }
        public SystemUser PostedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public ProjectData Project { get; set; }
        public string Text { get; set; }
        public string MoreInfo { get; set; }
        public PostTypesEnum PostType { get; set; }
        public StateEnum Status { get; set; }
    }
}
