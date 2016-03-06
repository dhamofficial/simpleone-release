using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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

            //master.Customers.Insert(0,new Customer { Name="All" });
            //master.Environments.Insert(0,new Environment { Name="All" });
            //master.Locations.Insert(0,new Location { Name="All" });
            //master.ReleaseTypes.Insert(0,new ReleaseType { Name= "All" });
            //master.Subscriptions.Insert(0,new Subscription { Name= "All" });

            return master;
        }

        public List<ReleaseItem> GetRecentReleases(paramproperty param=null)
        {
            int count = 10;
            //var releases = this.ReleaseItems.OrderByDescending(o => o.Id).Take(count).ToList();

            var sql = new StringBuilder();

            sql.AppendFormat(@" SELECT top {0} a.[Id] ,[Customer],[Version],[BuildDate],[BuildNumber],[Environment]
                              ,[ReleaseType],[CloudURL],[DNSURL],[MobileURL],[DBServer],[DBName],[Modules]
                              ,[Location],[Subscription],[Hostname],[VMSize],[SharedInstance],[NumberOfInstances]
                              ,ISNULL(b.name,'All') [CustomerName],c.Name [ReleaseTypeName],d.Name [EnvironmentName],e.Name [LocationName],f.Name [SubscriptionName]
                          FROM [dbo].[ReleaseItems] a
                          left outer Join Customers b on b.Id=a.Customer
                          left outer join ReleaseTypes c on c.Id =a.ReleaseType
                          left outer join Environments d on d.Id =a.Environment
                          left outer join Locations e on e.Id = a.Location
                          left outer join Subscriptions f on f.Id = a.Subscription where 1=1
                        ",count);

            if (param != null)
            {
                if (param.Id > 0)
                    sql.AppendFormat(" and a.Id={0} ", param.Id);
                if (param.Customer > 0)
                    sql.AppendFormat(" and a.Customer={0} ", param.Customer);
                if (param.Environment > 0)
                    sql.AppendFormat(" and a.Environment={0} ", param.Environment);
                if (param.ReleaseType> 0)
                    sql.AppendFormat(" and a.ReleaseType={0} ", param.ReleaseType);
                if (string.IsNullOrEmpty(param.BuildDate) == false)
                    sql.AppendFormat(" and a.BuildDate='{0}'",param.BuildDate);

            }

            var releases = ReleaseItems.SqlQuery(sql.ToString()).ToList<ReleaseItem>();

            return releases;
        }

    }
}