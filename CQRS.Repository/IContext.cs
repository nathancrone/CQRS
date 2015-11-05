using System;
using System.Data.Entity;
using CQRS.Core.Models;

namespace CQRS.Repository
{
    public interface IContext : IDisposable
    {
        DbSet<Task> Tasks { get; set; }
    }
}
