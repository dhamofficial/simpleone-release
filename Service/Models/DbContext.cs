using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(): base("name=DefaultConnection")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Environment> Environments { get; set; }
        public DbSet<ReleaseType> ReleaseTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ReleaseItem> ReleaseItems { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

    }
}