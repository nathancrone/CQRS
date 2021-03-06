namespace CQRS.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
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
            context.Processes.AddOrUpdate(
                x => x.Name,
                new Process { ProcessId = 1, Name = "Chris' Process" }
                );

            context.StateTypes.AddOrUpdate(
                x => x.Name,
                new StateType { StateTypeId = 1, Name = "Start" },
                new StateType { StateTypeId = 2, Name = "Normal" },
                new StateType { StateTypeId = 3, Name = "Complete" },
                new StateType { StateTypeId = 4, Name = "Denied" },
                new StateType { StateTypeId = 5, Name = "Cancelled" }
                );

            context.States.AddOrUpdate(
                x => x.Name,
                new State { StateId = 1, ProcessId = 1, StateTypeId = 1, Name = "Pending", Description = "Pending", X = 34, Y = 34 },
                new State { StateId = 2, ProcessId = 1, StateTypeId = 2, Name = "Verified", Description = "Verified", X = 221, Y = 82 },
                new State { StateId = 3, ProcessId = 1, StateTypeId = 2, Name = "Verified Ready", Description = "Verified Ready", X = 421, Y = 145 },
                new State { StateId = 4, ProcessId = 1, StateTypeId = 2, Name = "Verified Submitted", Description = "Verified Submitted", X = 644, Y = 198 }//,
                //new State { StateId = 5, ProcessId = 1, StateTypeId = 2, Name = "TMO Approved", Description = "TMO Approved", X = 854, Y = 122 },
                //new State { StateId = 6, ProcessId = 1, StateTypeId = 2, Name = "TMO Rejected", Description = "TMO Rejected", X = 854, Y = 280 },
                //new State { StateId = 7, ProcessId = 1, StateTypeId = 2, Name = "Approved", Description = "Approved", X = 1099, Y = 57 },
                //new State { StateId = 8, ProcessId = 1, StateTypeId = 2, Name = "Rejected", Description = "Rejected", X = 1099, Y = 189 }
                );

            context.ActionTypes.AddOrUpdate(
                x => x.Name, 
                new ActionType { ActionTypeId = 1, Name = "AWS Activity Complete" }, 
                new ActionType { ActionTypeId = 2, Name = "AWS Timer Elapse" }
                );

            Core.Models.Action a1 = new Core.Models.Action { ActionId = 1, ProcessId = 1, ActionTypeId = 1, Name = "AWS Activity Completed - 'Pending'", Description = "desc", Transitions = new List<Transition>() };
            Core.Models.Action a2 = new Core.Models.Action { ActionId = 2, ProcessId = 1, ActionTypeId = 1, Name = "AWS Activity Completed - 'Verified'", Description = "desc", Transitions = new List<Transition>() };
            Core.Models.Action a3 = new Core.Models.Action { ActionId = 3, ProcessId = 1, ActionTypeId = 1, Name = "AWS Activity Completed - 'Verified Ready'", Description = "desc", Transitions = new List<Transition>() };
            Core.Models.Action a4 = new Core.Models.Action { ActionId = 4, ProcessId = 1, ActionTypeId = 2, Name = "AWS Timer Elapsed - '10 Seconds'", Description = "desc", Transitions = new List<Transition>() };
            ////Core.Models.Action a5 = new Core.Models.Action { ActionId = 5, ProcessId = 1, ActionTypeId = 1, Name = "The import sets moves entry state to 'TMO Approved'", Description = "desc", Transitions = new List<Transition>() };
            ////Core.Models.Action a6 = new Core.Models.Action { ActionId = 6, ProcessId = 1, ActionTypeId = 1, Name = "The import sets moves entry state to 'TMO Rejected'", Description = "desc", Transitions = new List<Transition>() };
            //Core.Models.Action a7 = new Core.Models.Action { ActionId = 7, ProcessId = 1, ActionTypeId = 1, Name = "AWS Activity Completed - 'TMO Approved'", Description = "desc", Transitions = new List<Transition>() };
            ////Core.Models.Action a8 = new Core.Models.Action { ActionId = 8, ProcessId = 1, ActionTypeId = 1, Name = "Approver moves entry state to 'Approved'", Description = "desc", Transitions = new List<Transition>() };
            ////Core.Models.Action a9 = new Core.Models.Action { ActionId = 9, ProcessId = 1, ActionTypeId = 1, Name = "Approver moves entry state to 'Rejected'", Description = "desc", Transitions = new List<Transition>() };

            Transition t1 = new Transition { TransitionId = 1, ProcessId = 1, CurrentStateId = 1, NextStateId = 2 };
            Transition t2 = new Transition { TransitionId = 2, ProcessId = 1, CurrentStateId = 2, NextStateId = 3 };
            Transition t3 = new Transition { TransitionId = 3, ProcessId = 1, CurrentStateId = 3, NextStateId = 4 };
            //Transition t4 = new Transition { TransitionId = 4, ProcessId = 1, CurrentStateId = 4, NextStateId = 5 };
            //Transition t5 = new Transition { TransitionId = 5, ProcessId = 1, CurrentStateId = 4, NextStateId = 6 };
            //Transition t6 = new Transition { TransitionId = 6, ProcessId = 1, CurrentStateId = 5, NextStateId = 7 };
            //Transition t7 = new Transition { TransitionId = 6, ProcessId = 1, CurrentStateId = 5, NextStateId = 8 };

            a1.Transitions.Add(t1);
            a2.Transitions.Add(t2);
            a3.Transitions.Add(t3);
            a4.Transitions.Add(t1);
            ////a5.Transitions.Add(t4);
            //a4.Transitions.Add(t5);
            ////a6.Transitions.Add(t5);
            //a7.Transitions.Add(t6);
            ////a8.Transitions.Add(t6);
            //a7.Transitions.Add(t7);
            ////a9.Transitions.Add(t7);

            context.Actions.Add(a1);
            context.Actions.Add(a2);
            context.Actions.Add(a3);
            context.Actions.Add(a4);
            ////context.Actions.Add(a5);
            ////context.Actions.Add(a6);
            //context.Actions.Add(a7);
            ////context.Actions.Add(a8);
            ////context.Actions.Add(a9);
            
            context.Requests.AddOrUpdate(
                x => x.Title,
                new Request { RequestId = 1, ProcessId = 1, Title = "First Request", DateRequested = DateTime.Now }
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
