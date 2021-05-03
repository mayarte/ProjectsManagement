using Entity.MyData;
using System.Data.Entity;

namespace Entity
{
    public class TheDBContext : DbContext
    {
        public DbSet<SystemUser> Users { get; set; }
        public DbSet<SystemUserGroup> Groups { get; set; }
        public DbSet<Clearance> Clearances { get; set; }
        public DbSet<ProjectData> ProjectsData { get; set; }
        public DbSet<ProjectPost> ProjectPosts { get; set; }
        public DbSet<StudyYear> StudyYears { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures many-to-many relationship
            modelBuilder.Entity<SystemUserGroup>().HasMany(c => c.Clearances).WithMany(x => x.Groups);
        }
    }
}
