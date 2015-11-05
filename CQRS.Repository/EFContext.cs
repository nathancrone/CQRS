using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CQRS.Core.Models;

namespace CQRS.Repository
{
    class EFContext : DbContext, IContext
    {
        public virtual DbSet<Task> Tasks { get; set; }

        //using fluent api instead of attributes (avoids decorating POCO class with EF-specific attributes)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //On each entity, we need to define a key column.
            modelBuilder.Entity<Task>().HasKey(x => x.TaskId);

            //specify that the keys are IDENTITY columns
            modelBuilder.Entity<Task>()
                .Property(x => x.TaskId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
