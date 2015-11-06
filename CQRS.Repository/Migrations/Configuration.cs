namespace CQRS.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CQRS.Core.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CQRS.Repository.EFContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CQRS.Repository.EFContext";
        }

        protected override void Seed(CQRS.Repository.EFContext context)
        {
            context.Tasks.AddOrUpdate(
                x => x.Title,
                new Task { Title = "Task 1" },
                new Task { Title = "Task 2" },
                new Task { Title = "Task 3" }
            );

            //  This method will be called after migrating to the latest version.

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
