using Shiftim.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shiftim.Dal
{
    public class UserDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //  base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<User>().ToTable("users");

            //configure email as PK for ...
            modelBuilder.Entity<Worker>().HasKey(e => e.email);

            //configure email as FK for ...
            modelBuilder.Entity<User>().HasRequired(e => e.worker).WithRequiredPrincipal(us => us.user1);

        }
        public DbSet<User> Users { get; set; }
     //   public DbSet<User> Wo { get; set; }

        public List<User> GetAllUsers()
        {
            return Users.ToList();
        }

    }
}