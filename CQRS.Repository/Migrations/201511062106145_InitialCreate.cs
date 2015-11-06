namespace CQRS.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        ActionId = c.Int(nullable: false, identity: true),
                        ProcessId = c.Int(nullable: false),
                        ActionTypeId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 100),
                        State_StateId = c.Int(),
                    })
                .PrimaryKey(t => t.ActionId)
                .ForeignKey("dbo.ActionTypes", t => t.ActionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Processes", t => t.ProcessId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.State_StateId)
                .Index(t => t.ProcessId)
                .Index(t => t.ActionTypeId)
                .Index(t => t.State_StateId);
            
            CreateTable(
                "dbo.ActionTypes",
                c => new
                    {
                        ActionTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ActionTypeId);
            
            CreateTable(
                "dbo.Processes",
                c => new
                    {
                        ProcessId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ProcessId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        ProcessId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.Processes", t => t.ProcessId, cascadeDelete: true)
                .Index(t => t.ProcessId);
            
            CreateTable(
                "dbo.ActionTargets",
                c => new
                    {
                        ActionTargetId = c.Int(nullable: false, identity: true),
                        ActionId = c.Int(),
                        TargetId = c.Int(nullable: false),
                        GroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ActionTargetId)
                .ForeignKey("dbo.Actions", t => t.ActionId)
                .ForeignKey("dbo.Targets", t => t.TargetId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.ActionId)
                .Index(t => t.TargetId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Targets",
                c => new
                    {
                        TargetId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.TargetId);
            
            CreateTable(
                "dbo.ActivityTargets",
                c => new
                    {
                        ActivityTargetId = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(),
                        TargetId = c.Int(nullable: false),
                        GroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ActivityTargetId)
                .ForeignKey("dbo.Activities", t => t.ActivityId)
                .ForeignKey("dbo.Targets", t => t.TargetId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.ActivityId)
                .Index(t => t.TargetId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        ProcessId = c.Int(),
                        ActivityTypeId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ActivityId)
                .ForeignKey("dbo.ActivityTypes", t => t.ActivityTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Processes", t => t.ProcessId)
                .Index(t => t.ProcessId)
                .Index(t => t.ActivityTypeId);
            
            CreateTable(
                "dbo.ActivityTypes",
                c => new
                    {
                        ActivityTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ActivityTypeId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        ProcessId = c.Int(nullable: false),
                        StateTypeId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.StateTypes", t => t.StateTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Processes", t => t.ProcessId, cascadeDelete: true)
                .Index(t => t.ProcessId)
                .Index(t => t.StateTypeId);
            
            CreateTable(
                "dbo.StateTypes",
                c => new
                    {
                        StateTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.StateTypeId);
            
            CreateTable(
                "dbo.Transitions",
                c => new
                    {
                        TransitionId = c.Int(nullable: false, identity: true),
                        ProcessId = c.Int(nullable: false),
                        CurrentStateId = c.Int(),
                        NextStateId = c.Int(nullable: false),
                        CurrentState_StateId = c.Int(),
                    })
                .PrimaryKey(t => t.TransitionId)
                .ForeignKey("dbo.States", t => t.CurrentState_StateId)
                .ForeignKey("dbo.States", t => t.NextStateId, cascadeDelete: true)
                .ForeignKey("dbo.Processes", t => t.ProcessId)
                .Index(t => t.ProcessId)
                .Index(t => t.NextStateId)
                .Index(t => t.CurrentState_StateId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.RequestNotes",
                c => new
                    {
                        RequestNoteId = c.Int(nullable: false, identity: true),
                        RequestId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Note = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.RequestNoteId)
                .ForeignKey("dbo.Requests", t => t.RequestId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RequestId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        ProcessId = c.Int(),
                        UserId = c.Int(),
                        Title = c.String(nullable: false, maxLength: 100),
                        DateRequested = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Processes", t => t.ProcessId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.ProcessId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.RequestDatas",
                c => new
                    {
                        RequestDataId = c.Int(nullable: false, identity: true),
                        RequestId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Value = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.RequestDataId)
                .ForeignKey("dbo.Requests", t => t.RequestId, cascadeDelete: true)
                .Index(t => t.RequestId);
            
            CreateTable(
                "dbo.RequestActions",
                c => new
                    {
                        RequestActionId = c.Int(nullable: false, identity: true),
                        RequestId = c.Int(),
                        ActionId = c.Int(),
                        TransitionId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsComplete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RequestActionId)
                .ForeignKey("dbo.Actions", t => t.ActionId)
                .ForeignKey("dbo.Requests", t => t.RequestId)
                .ForeignKey("dbo.Transitions", t => t.TransitionId)
                .Index(t => t.RequestId)
                .Index(t => t.ActionId)
                .Index(t => t.TransitionId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.TaskId);
            
            CreateTable(
                "dbo.StateActivity",
                c => new
                    {
                        ActivityId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ActivityId, t.StateId })
                .ForeignKey("dbo.States", t => t.ActivityId)
                .ForeignKey("dbo.Activities", t => t.StateId)
                .Index(t => t.ActivityId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.TransitionAction",
                c => new
                    {
                        ActionId = c.Int(nullable: false),
                        TransitionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ActionId, t.TransitionId })
                .ForeignKey("dbo.Transitions", t => t.ActionId)
                .ForeignKey("dbo.Actions", t => t.TransitionId)
                .Index(t => t.ActionId)
                .Index(t => t.TransitionId);
            
            CreateTable(
                "dbo.TransitionActivity",
                c => new
                    {
                        ActivityId = c.Int(nullable: false),
                        TransitionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ActivityId, t.TransitionId })
                .ForeignKey("dbo.Transitions", t => t.ActivityId)
                .ForeignKey("dbo.Activities", t => t.TransitionId)
                .Index(t => t.ActivityId)
                .Index(t => t.TransitionId);
            
            CreateTable(
                "dbo.RequestStakeholder",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RequestId })
                .ForeignKey("dbo.Requests", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.RequestId)
                .Index(t => t.UserId)
                .Index(t => t.RequestId);
            
            CreateTable(
                "dbo.GroupMember",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.Groups", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.GroupId)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.ProcessAdmin",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProcessId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProcessId })
                .ForeignKey("dbo.Processes", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.ProcessId)
                .Index(t => t.UserId)
                .Index(t => t.ProcessId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestActions", "TransitionId", "dbo.Transitions");
            DropForeignKey("dbo.RequestActions", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.RequestActions", "ActionId", "dbo.Actions");
            DropForeignKey("dbo.Transitions", "ProcessId", "dbo.Processes");
            DropForeignKey("dbo.States", "ProcessId", "dbo.Processes");
            DropForeignKey("dbo.ProcessAdmin", "ProcessId", "dbo.Users");
            DropForeignKey("dbo.ProcessAdmin", "UserId", "dbo.Processes");
            DropForeignKey("dbo.Groups", "ProcessId", "dbo.Processes");
            DropForeignKey("dbo.GroupMember", "GroupId", "dbo.Users");
            DropForeignKey("dbo.GroupMember", "UserId", "dbo.Groups");
            DropForeignKey("dbo.RequestNotes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Requests", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.RequestStakeholder", "RequestId", "dbo.Users");
            DropForeignKey("dbo.RequestStakeholder", "UserId", "dbo.Requests");
            DropForeignKey("dbo.RequestNotes", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.RequestDatas", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.Requests", "ProcessId", "dbo.Processes");
            DropForeignKey("dbo.ActivityTargets", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.ActionTargets", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.ActivityTargets", "TargetId", "dbo.Targets");
            DropForeignKey("dbo.ActivityTargets", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Transitions", "NextStateId", "dbo.States");
            DropForeignKey("dbo.Transitions", "CurrentState_StateId", "dbo.States");
            DropForeignKey("dbo.TransitionActivity", "TransitionId", "dbo.Activities");
            DropForeignKey("dbo.TransitionActivity", "ActivityId", "dbo.Transitions");
            DropForeignKey("dbo.TransitionAction", "TransitionId", "dbo.Actions");
            DropForeignKey("dbo.TransitionAction", "ActionId", "dbo.Transitions");
            DropForeignKey("dbo.States", "StateTypeId", "dbo.StateTypes");
            DropForeignKey("dbo.StateActivity", "StateId", "dbo.Activities");
            DropForeignKey("dbo.StateActivity", "ActivityId", "dbo.States");
            DropForeignKey("dbo.Actions", "State_StateId", "dbo.States");
            DropForeignKey("dbo.Activities", "ProcessId", "dbo.Processes");
            DropForeignKey("dbo.Activities", "ActivityTypeId", "dbo.ActivityTypes");
            DropForeignKey("dbo.ActionTargets", "TargetId", "dbo.Targets");
            DropForeignKey("dbo.ActionTargets", "ActionId", "dbo.Actions");
            DropForeignKey("dbo.Actions", "ProcessId", "dbo.Processes");
            DropForeignKey("dbo.Actions", "ActionTypeId", "dbo.ActionTypes");
            DropIndex("dbo.ProcessAdmin", new[] { "ProcessId" });
            DropIndex("dbo.ProcessAdmin", new[] { "UserId" });
            DropIndex("dbo.GroupMember", new[] { "GroupId" });
            DropIndex("dbo.GroupMember", new[] { "UserId" });
            DropIndex("dbo.RequestStakeholder", new[] { "RequestId" });
            DropIndex("dbo.RequestStakeholder", new[] { "UserId" });
            DropIndex("dbo.TransitionActivity", new[] { "TransitionId" });
            DropIndex("dbo.TransitionActivity", new[] { "ActivityId" });
            DropIndex("dbo.TransitionAction", new[] { "TransitionId" });
            DropIndex("dbo.TransitionAction", new[] { "ActionId" });
            DropIndex("dbo.StateActivity", new[] { "StateId" });
            DropIndex("dbo.StateActivity", new[] { "ActivityId" });
            DropIndex("dbo.RequestActions", new[] { "TransitionId" });
            DropIndex("dbo.RequestActions", new[] { "ActionId" });
            DropIndex("dbo.RequestActions", new[] { "RequestId" });
            DropIndex("dbo.RequestDatas", new[] { "RequestId" });
            DropIndex("dbo.Requests", new[] { "User_UserId" });
            DropIndex("dbo.Requests", new[] { "ProcessId" });
            DropIndex("dbo.RequestNotes", new[] { "UserId" });
            DropIndex("dbo.RequestNotes", new[] { "RequestId" });
            DropIndex("dbo.Transitions", new[] { "CurrentState_StateId" });
            DropIndex("dbo.Transitions", new[] { "NextStateId" });
            DropIndex("dbo.Transitions", new[] { "ProcessId" });
            DropIndex("dbo.States", new[] { "StateTypeId" });
            DropIndex("dbo.States", new[] { "ProcessId" });
            DropIndex("dbo.Activities", new[] { "ActivityTypeId" });
            DropIndex("dbo.Activities", new[] { "ProcessId" });
            DropIndex("dbo.ActivityTargets", new[] { "GroupId" });
            DropIndex("dbo.ActivityTargets", new[] { "TargetId" });
            DropIndex("dbo.ActivityTargets", new[] { "ActivityId" });
            DropIndex("dbo.ActionTargets", new[] { "GroupId" });
            DropIndex("dbo.ActionTargets", new[] { "TargetId" });
            DropIndex("dbo.ActionTargets", new[] { "ActionId" });
            DropIndex("dbo.Groups", new[] { "ProcessId" });
            DropIndex("dbo.Actions", new[] { "State_StateId" });
            DropIndex("dbo.Actions", new[] { "ActionTypeId" });
            DropIndex("dbo.Actions", new[] { "ProcessId" });
            DropTable("dbo.ProcessAdmin");
            DropTable("dbo.GroupMember");
            DropTable("dbo.RequestStakeholder");
            DropTable("dbo.TransitionActivity");
            DropTable("dbo.TransitionAction");
            DropTable("dbo.StateActivity");
            DropTable("dbo.Tasks");
            DropTable("dbo.RequestActions");
            DropTable("dbo.RequestDatas");
            DropTable("dbo.Requests");
            DropTable("dbo.RequestNotes");
            DropTable("dbo.Users");
            DropTable("dbo.Transitions");
            DropTable("dbo.StateTypes");
            DropTable("dbo.States");
            DropTable("dbo.ActivityTypes");
            DropTable("dbo.Activities");
            DropTable("dbo.ActivityTargets");
            DropTable("dbo.Targets");
            DropTable("dbo.ActionTargets");
            DropTable("dbo.Groups");
            DropTable("dbo.Processes");
            DropTable("dbo.ActionTypes");
            DropTable("dbo.Actions");
        }
    }
}
