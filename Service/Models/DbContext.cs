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

            master.Customers.Insert(0, new Customer { Name = "All" });
            master.Environments.Insert(0, new Environment { Name = "All" });
            master.Locations.Insert(0, new Location { Name = "All" });
            master.ReleaseTypes.Insert(0, new ReleaseType { Name = "All" });
            master.Subscriptions.Insert(0, new Subscription { Name = "All" });

            return master;
        }

        public List<textvalueproperty> GetDashboardData()
        {
            var sql = new StringBuilder();
            sql.Append(@"    SELECT a.*
FROM (
	SELECT 'THIS WEEK' [text]
	,(case when count(case when a.Status='Planned' then 1 else null end)>0 then cast(count(case when a.Status='Planned' then 1 else null end) as varchar) + ' Planned ' else '' end
		+ case when count(case when a.Status='Released' then 1 else null end)>0 then cast(count(case when a.Status='Released' then 1 else null end) as varchar) + ' Released' else '' end 
	)[value]
		,'blue' [cssclass]
		,1 [sortorder]
		,'thisweek' [flag]
	From ReleaseItems a
	Where a.ReleaseDate >=dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) and a.ReleaseDate<=dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate()))

	UNION
	
	SELECT 'THIS MONTH' [text]
		,(case when count(case when a.Status='Planned' then 1 else null end)>0 then cast(count(case when a.Status='Planned' then 1 else null end) as varchar) + ' Planned ' else '' end
		+ case when count(case when a.Status='Released' then 1 else null end)>0 then cast(count(case when a.Status='Released' then 1 else null end) as varchar) + ' Released' else '' end 
	)[value]
		,'red' [cssclass]
		,2 [sortorder]
		,'thismonth' [flag]
	From ReleaseItems a
	Where datepart(month,a.ReleaseDate)=datepart(MONTH,getdate())
	
	UNION
	
	SELECT 'NEXT MONTH' [text]
		,(case when count(case when isnull(a.Status,'Planned')='Planned' then 1 else null end)>0 then cast(count(case when isnull(a.Status,'Planned')='Planned' then 1 else null end) as varchar) + ' Planned ' else '' end
		+ case when count(case when a.Status='Released' then 1 else null end)>0 then cast(count(case when a.Status='Released' then 1 else null end) as varchar) + ' Released' else '' end 
	)[value]
		,'purple' [cssclass]
		,3 [sortorder]
		,'nextmonth' [flag]
	From ReleaseItems a
	Where datepart(month,a.ReleaseDate)=datepart(MONTH,getdate())+1
	
	UNION
	
	SELECT 'OLD RELEASES' [text]
		,(cast(count(case when a.Status='Released' then 1 else null end) as varchar) + ' Released') [value]
		,'green' [cssclass]
		,4 [sortorder]
		,'oldreleases' [flag]
	From ReleaseItems a
	Where datepart(month,a.ReleaseDate)<datepart(MONTH,getdate())
	) a
ORDER BY a.sortorder


select ReleaseDate, * from ReleaseItems ");

            var list = Database.SqlQuery<textvalueproperty>(sql.ToString()).ToList<textvalueproperty>();
            return list;
        }

        public List<ReleaseItem> GetRecentReleases(paramproperty param=null,bool getAll=false)
        {
            int count = 5;
            //var releases = this.ReleaseItems.OrderByDescending(o => o.Id).Take(count).ToList();

            var sql = new StringBuilder();

            sql.Append(" select ");

            if (!getAll)
                sql.AppendFormat(" top {0} ", count);

            sql.AppendFormat(@" a.[Id] ,[Customer],[Version],[BuildDate],[BuildNumber],[Environment]
                              ,[ReleaseType],[CloudURL],[DNSURL],[MobileURL],[DBServer],[DBName],[Modules]
                              ,[Location],[Subscription],[Hostname],[VMSize],[SharedInstance],[NumberOfInstances]
                              ,ISNULL(b.name,'All') [CustomerName],c.Name [ReleaseTypeName],d.Name [EnvironmentName],e.Name [LocationName],f.Name [SubscriptionName]
                            ,a.ReleaseDate,a.LicenseExpiryDate,a.Status
                          FROM [dbo].[ReleaseItems] a
                          left outer Join Customers b on b.Id=a.Customer
                          left outer join ReleaseTypes c on c.Id =a.ReleaseType
                          left outer join Environments d on d.Id =a.Environment
                          left outer join Locations e on e.Id = a.Location
                          left outer join Subscriptions f on f.Id = a.Subscription where 1=1
                        ", count);

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
                if (string.IsNullOrEmpty(param.ReleaseDate) == false)
                    sql.AppendFormat(" and a.ReleaseDate='{0}'", param.ReleaseDate);

                if (param.flag == "thisweek")
                    sql.Append(" and a.ReleaseDate >=dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) and a.ReleaseDate<=dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate()))  ");

                else if (param.flag == "thismonth")
                    sql.Append(" and datepart(month,a.ReleaseDate)=datepart(MONTH,getdate()) ");

                else if (param.flag == "nextmonth")
                    sql.Append(" and datepart(month,a.ReleaseDate)=datepart(MONTH,getdate())+1 ");

                else if (param.flag == "oldreleases")
                    sql.Append(" and datepart(month,a.ReleaseDate)<datepart(MONTH,getdate()) ");

            }

            var releases = ReleaseItems.SqlQuery(sql.ToString()).ToList<ReleaseItem>();

            return releases;
        }

    }
}