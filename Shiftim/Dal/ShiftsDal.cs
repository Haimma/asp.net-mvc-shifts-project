using Shiftim.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shiftim.Dal
{
    public class ShiftsDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Shifts>().ToTable("shifts");
        }
        public DbSet<Shifts> Shifts {get; set;}

    }
}