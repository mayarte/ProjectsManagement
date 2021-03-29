using Entity.MyData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class TheDBContext : DbContext
    {
        public DbSet<SystemUser> Users { get; set; }
        public DbSet<SystemUserGroup> Groups { get; set; }
        public DbSet<Clearance> Clearances { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures many-to-many relationship
            // modelBuilder.Entity<Clearance>().HasMany(c => c.Groups).WithMany(g => g.Clearances);
        }
    }
}
