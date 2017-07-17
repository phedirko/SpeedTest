using SpeedTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SpeedTest
{
    public class STdbcontext : DbContext
    {
        public STdbcontext()
            :base("DbConnection")
        { }

        public DbSet<Site> Sites { get; set; }

        public DbSet<Url> Urls { get; set; }

        public DbSet<Measurement> Measurements { get; set; }
    }
}