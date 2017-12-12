using Shiftim.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shiftim.Dal
{
    public class MSGDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MSG>().ToTable("message");
        }
        public DbSet<MSG> Messages { get; set; }
        //   public DbSet<User> Wo { get; set; }

        //public List<User> GetAllUsers()
        //{
        //    return Users.ToList();
        //}

    }
}