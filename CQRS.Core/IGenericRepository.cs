using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CQRS.Core
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        IList<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);

        //Query Methods
        T FindById(int id);

        //Insert/Update/Delete
        void Insert(T entity);
        void Update(T entity, string[] Properties = null);
        void Delete(T entity);

        //Persistence
        void Save();
    }
}
