using System;
using System.Data.Entity;
using CQRS.Core.Models;

namespace CQRS.Repository
{
    public interface IContext : IDisposable
    {
        DbSet<Task> Tasks { get; set; }

        DbSet<Process> Processes { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Request> Requests { get; set; }
        DbSet<RequestNote> RequestNotes { get; set; }
        DbSet<RequestData> RequestData { get; set; }
        DbSet<StateType> StateTypes { get; set; }
        DbSet<State> States { get; set; }
        DbSet<Transition> Transitions { get; set; }
        DbSet<ActionType> ActionTypes { get; set; }
        DbSet<CQRS.Core.Models.Action> Actions { get; set; }
        DbSet<ActivityType> ActivityTypes { get; set; }
        DbSet<Activity> Activities { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<Target> Targets { get; set; }
        DbSet<ActionTarget> ActionTargets { get; set; }
        DbSet<ActivityTarget> ActivityTargets { get; set; }
        DbSet<RequestAction> RequestActions { get; set; }
    }
}
