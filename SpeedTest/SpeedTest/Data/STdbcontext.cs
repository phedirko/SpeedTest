using SpeedTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection.Emit;

namespace SpeedTest
{
    public class STdbcontext : DbContext
    {
        public STdbcontext()
            :base("DbConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public DbSet<Site> Sites { get; set; }

        public DbSet<Url> Urls { get; set; }

        public DbSet<Measurement> Measurements { get; set; }
    }
}