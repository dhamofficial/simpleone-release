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
                new Models.Customer { Name="All" , Active=true}
            );

            context.Environments.AddOrUpdate(p => p.Name,
                new Models.Environment { Name = "All" }
            );

            context.Locations.AddOrUpdate(p => p.Name,
                new Models.Location { Name = "All" }
            );

            context.ReleaseTypes.AddOrUpdate(p => p.Name,
                new Models.ReleaseType { Name = "All" }
            );

            context.Subscriptions.AddOrUpdate(p => p.Name,
                new Models.Subscription { Name = "All" }
            );

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
