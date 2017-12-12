using Shiftim.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shiftim.Dal
{
    public class WorkerDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //configure email as PK for ...
            modelBuilder.Entity<Worker>().HasKey(e => e.email);
            //configure email as FK for ...
            modelBuilder.Entity<User>().HasRequired(e => e.worker).WithRequiredPrincipal(us=>us.user1);

            this.Configuration.ProxyCreationEnabled = true;
        }

   
        public DbSet<Worker> Workers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}