namespace Entity.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Entity.TheDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Entity.TheDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var year = 2015;

            for (int i = 1; i < 10; i++)
            {
                context.StudyYears.AddOrUpdate(new MyData.StudyYear() { ID = i, Year1 = year.ToString(), Year2 = (year + 1).ToString() });
            }

            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 1, Name = "Clearances" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 2, Name = "SystemUserGroups" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 3, Name = "SystemUsers" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 4, Name = "StudyYears" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 5, Name = "ProjectPosts" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 6, Name = "ProjectDatas" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 7, Name = "ProposeProject" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 8, Name = "ProjectList" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 9, Name = "Home" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 10, Name = "MyProjects" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 11, Name = "Projects" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 12, Name = "AddPost" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 13, Name = "DeletePost" });
            context.Clearances.AddOrUpdate(new MyData.Clearance() { ID = 14, Name = "EditPost" });

            var adminList = new List<string>()
            {
                "Clearances",
                "SystemUserGroups",
                "SystemUsers",
                "StudyYears",
                "ProjectPosts",
                "ProjectDatas",
                "ProposeProject",
                "ProjectList",
                "Home",
                "MyProjects",
                "Projects",
                "AddPost",
                "DeletePost",
                "EditPost"
            };
            var adminClerances = context.Clearances.Where(x => adminList.Contains(x.Name)).ToList();
            context.Groups.AddOrUpdate(new MyData.SystemUserGroup() { ID = 1, Name = "الادمنية", Clearances= adminClerances });

            var stList = new List<string>()
            {
                "ProjectList",
                "Home",
                "MyProjects",
                "Projects",
                "AddPost",
                "DeletePost",
                "EditPost"
            };
            var stClerances = context.Clearances.Where(x => adminList.Contains(x.Name)).ToList();
            context.Groups.AddOrUpdate(new MyData.SystemUserGroup() { ID = 2, Name = "الطلبة", Clearances = stClerances });

            var supervisorList = new List<string>()
            {
                "ProposeProject",
                "ProjectList",
                "Home",
                "MyProjects",
                "Projects",
                "AddPost",
                "DeletePost",
                "EditPost"
            };
            var supervisorClerances = context.Clearances.Where(x => adminList.Contains(x.Name)).ToList();
            context.Groups.AddOrUpdate(new MyData.SystemUserGroup() { ID = 3, Name = "المشرفين", Clearances = supervisorClerances });

            context.Users.AddOrUpdate(new MyData.SystemUser() { ID = 1, Name = "admin", Password = "admin" });
        }
    }
}
