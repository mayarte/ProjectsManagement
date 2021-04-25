﻿namespace Entity.MyData
{
    public class SystemUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public SystemUserGroup ClearanceGroup { get; set; }
    }
}
