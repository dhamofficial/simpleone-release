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

        public Masters GetMasters()
        {
            var master = new Masters();

            master.Customers = Customers.ToList();
            master.Environments = Environments.ToList();
            master.Locations = Locations.ToList();
            master.ReleaseTypes = ReleaseTypes.ToList();
            master.Subscriptions = Subscriptions.ToList();

            return master;
        }

        public List<ReleaseItem> GetRecentReleases()
        {
            int count = 10;
            var releases = this.ReleaseItems.OrderByDescending(o => o.Id).Take(count).ToList();

            //var releases = (from a in ReleaseItems
            //                  join b in Customers on a.Customer equals b.Id
            //                  orderby a.Id descending
            //                  select new ReleaseItem
            //                  {
            //                       CustomerName = a.CustomerName,
            //                       Customer=a.Customer,
            //                       Version = a.Version,
            //                       BuildDate = a.BuildDate,
            //                       BuildNumber=a.BuildNumber,
            //                       Environment = a.Environment,
            //                       ReleaseType = a.ReleaseType,

            //                  }).Take(count).ToList();

            return releases;
        }

    }
}