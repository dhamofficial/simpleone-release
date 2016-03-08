namespace Service.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Service.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

        }

        protected override void Seed(Service.Models.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.


            context.Customers.AddOrUpdate(p=>p.Name,
                new Models.Customer { Name="KPMG" , Active=true},
                new Models.Customer { Name = "Dell", Active = true },
                new Models.Customer { Name = "IBM", Active = true }
            );

            context.Environments.AddOrUpdate(p => p.Name,
                new Models.Environment { Name = "Cloud" },
                new Models.Environment { Name = "On-Premise" }
            );


            context.Locations.AddOrUpdate(p => p.Name,
                new Models.Location { Name = "Bangalore" },
                new Models.Location { Name = "Chennai" },
                new Models.Location { Name = "Mumbai" },
                new Models.Location { Name = "Delhi" }
            );

            context.ReleaseTypes.AddOrUpdate(p => p.Name,
                new Models.ReleaseType { Name = "POC" },
                new Models.ReleaseType { Name = "Implementation" }
            );

            //context.Subscriptions.AddOrUpdate(p => p.Name,
            //    new Models.Subscription { Name = "All" }
            //);

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}



//drop table[dbo].[Customers]
//drop table[dbo].[Environments]
//drop table[dbo].[Locations]
//drop table[dbo].[ReleaseItems]
//drop table[dbo].[ReleaseTypes]
//drop table[dbo].[Subscriptions]