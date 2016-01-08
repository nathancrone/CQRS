using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CQRS.Core;
using CQRS.Core.Models;

namespace CQRS.Repository
{
    public class EFContext : DbContext, IUnitOfWork
    {

        public EFContext() : base("name=WFModel")
        {
        }

        public virtual DbSet<Task> Tasks { get; set; }

        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestNote> RequestNotes { get; set; }
        public virtual DbSet<RequestData> RequestData { get; set; }
        public virtual DbSet<StateType> StateTypes { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Transition> Transitions { get; set; }
        public virtual DbSet<ActionType> ActionTypes { get; set; }
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<ActivityType> ActivityTypes { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Target> Targets { get; set; }
        public virtual DbSet<ActionTarget> ActionTargets { get; set; }
        public virtual DbSet<ActivityTarget> ActivityTargets { get; set; }
        public virtual DbSet<RequestAction> RequestActions { get; set; }

        public override int SaveChanges()
        {
            Configuration.ValidateOnSaveEnabled = false;
            return base.SaveChanges();
        }

        //using fluent api instead of attributes (avoids decorating POCO class with EF-specific attributes)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //On each entity, we need to define a key column.
            modelBuilder.Entity<Task>().HasKey(x => x.TaskId);

            modelBuilder.Entity<Process>().HasKey(x => x.ProcessId);
            modelBuilder.Entity<User>().HasKey(x => x.UserId);
            modelBuilder.Entity<Request>().HasKey(x => x.RequestId);
            modelBuilder.Entity<RequestNote>().HasKey(x => x.RequestNoteId);
            modelBuilder.Entity<RequestData>().HasKey(x => x.RequestDataId);
            modelBuilder.Entity<StateType>().HasKey(x => x.StateTypeId);
            modelBuilder.Entity<State>().HasKey(x => x.StateId);
            modelBuilder.Entity<Transition>().HasKey(x => x.TransitionId);
            modelBuilder.Entity<ActionType>().HasKey(x => x.ActionTypeId);
            modelBuilder.Entity<Action>().HasKey(x => x.ActionId);
            modelBuilder.Entity<ActivityType>().HasKey(x => x.ActivityTypeId);
            modelBuilder.Entity<Activity>().HasKey(x => x.ActivityId);
            modelBuilder.Entity<Group>().HasKey(x => x.GroupId);
            modelBuilder.Entity<Target>().HasKey(x => x.TargetId);
            modelBuilder.Entity<ActionTarget>().HasKey(x => x.ActionTargetId);
            modelBuilder.Entity<ActivityTarget>().HasKey(x => x.ActivityTargetId);
            modelBuilder.Entity<RequestAction>().HasKey(x => x.RequestActionId);

            //specify that the keys are IDENTITY columns
            modelBuilder.Entity<Task>()
                .Property(x => x.TaskId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Process>()
                .Property(x => x.ProcessId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>()
                .Property(x => x.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Request>()
                .Property(x => x.RequestId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<RequestNote>()
                .Property(x => x.RequestNoteId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<RequestData>()
                .Property(x => x.RequestDataId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<StateType>()
                .Property(x => x.StateTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<State>()
                .Property(x => x.StateId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Transition>()
                .Property(x => x.TransitionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ActionType>()
                .Property(x => x.ActionTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Action>()
                .Property(x => x.ActionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ActivityType>()
                .Property(x => x.ActivityTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Activity>()
                .Property(x => x.ActivityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Group>()
                .Property(x => x.GroupId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Target>()
                .Property(x => x.TargetId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<RequestAction>()
                .Property(x => x.RequestActionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);            


            //for each property in an entity, define the attributes of the corresponding column (string length, nullable, etc).
            //process
            modelBuilder.Entity<Process>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            //user
            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            //request
            modelBuilder.Entity<Request>()
                .Property(x => x.DateRequested)
                .IsRequired();
            modelBuilder.Entity<Request>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            //request note
            modelBuilder.Entity<RequestNote>()
                .Property(x => x.Note)
                .IsRequired()
                .HasMaxLength(500);

            //request data
            modelBuilder.Entity<RequestData>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<RequestData>()
                .Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(100);

            //state type
            modelBuilder.Entity<StateType>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            //state
            modelBuilder.Entity<State>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<State>()
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100);

            //action type
            modelBuilder.Entity<ActionType>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            //action
            modelBuilder.Entity<Action>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Action>()
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100);

            //activity type
            modelBuilder.Entity<ActivityType>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            //activity
            modelBuilder.Entity<Activity>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Activity>()
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100);

            //group
            modelBuilder.Entity<Group>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            //target
            modelBuilder.Entity<Target>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Target>()
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100);

            //For each relationship, define the multiplicity on each side 
            modelBuilder.Entity<Process>()
                .HasMany(x => x.ProcessAdmins)
                .WithMany(x => x.AdministeredProcesses)
                .Map(c => c.MapLeftKey("UserId")
                .MapRightKey("ProcessId")
                .ToTable("ProcessAdmin"));

            modelBuilder.Entity<Process>()
                .HasMany(x => x.States)
                .WithRequired(x => x.Process);

            modelBuilder.Entity<Process>()
                .HasMany(x => x.Transitions)
                .WithRequired(x => x.Process)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Process>()
                .HasMany(x => x.Actions)
                .WithRequired(x => x.Process);

            modelBuilder.Entity<Process>()
                .HasMany(x => x.Groups)
                .WithRequired(x => x.Process);

            modelBuilder.Entity<Request>()
                .HasMany(x => x.Stakeholders)
                .WithMany(x => x.StakeRequests)
                .Map(c => c.MapLeftKey("UserId")
                .MapRightKey("RequestId")
                .ToTable("RequestStakeholder"));

            modelBuilder.Entity<Request>()
                .HasMany(x => x.RequestNotes)
                .WithRequired(x => x.Request);

            modelBuilder.Entity<Request>()
                .HasMany(x => x.RequestData)
                .WithRequired(x => x.Request);

            modelBuilder.Entity<User>()
                .HasMany(x => x.RequestNotes)
                .WithRequired(x => x.User);

            modelBuilder.Entity<Request>()
                .HasMany(x => x.RequestActions)
                .WithRequired(x => x.Request);

            modelBuilder.Entity<StateType>()
                .HasMany(x => x.States)
                .WithRequired(x => x.StateType);

            modelBuilder.Entity<State>()
                .HasMany(x => x.TransitionsFrom)
                .WithRequired(x => x.CurrentState)
                .HasForeignKey(x => x.CurrentStateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .HasMany(x => x.TransitionsTo)
                .WithRequired(x => x.NextState)
                .HasForeignKey(x => x.NextStateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ActionType>()
                .HasMany(x => x.Actions)
                .WithRequired(x => x.ActionType);

            modelBuilder.Entity<Transition>()
                .HasMany(x => x.Actions)
                .WithMany(x => x.Transitions)
                .Map(c => c.MapLeftKey("TransitionId")
                .MapRightKey("ActionId")
                .ToTable("TransitionAction"));

            modelBuilder.Entity<ActivityType>()
                .HasMany(x => x.Activities)
                .WithRequired(x => x.ActivityType);

            modelBuilder.Entity<Transition>()
                .HasMany(x => x.Activities)
                .WithMany(x => x.Transitions)
                .Map(c => c.MapLeftKey("ActivityId")
                .MapRightKey("TransitionId")
                .ToTable("TransitionActivity"));

            modelBuilder.Entity<State>()
                .HasMany(x => x.Activities)
                .WithMany(x => x.States)
                .Map(c => c.MapLeftKey("ActivityId")
                .MapRightKey("StateId")
                .ToTable("StateActivity"));

            modelBuilder.Entity<Group>()
                .HasMany(x => x.Members)
                .WithMany(x => x.Groups)
                .Map(c => c.MapLeftKey("UserId")
                .MapRightKey("GroupId")
                .ToTable("GroupMember"));

            modelBuilder.Entity<Group>()
                .HasMany(x => x.ActionTargets)
                .WithOptional(x => x.Group);

            modelBuilder.Entity<Group>()
                .HasMany(x => x.ActivityTargets)
                .WithOptional(x => x.Group);

            modelBuilder.Entity<Target>()
                .HasMany(x => x.ActionTargets)
                .WithRequired(x => x.Target);

            modelBuilder.Entity<Target>()
                .HasMany(x => x.ActivityTargets)
                .WithRequired(x => x.Target);
        }
    }
}
