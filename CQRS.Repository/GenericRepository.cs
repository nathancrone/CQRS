using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CQRS.Core;

namespace CQRS.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        IContext _context;
        DbSet<T> _dbSet;

        public GenericRepository(IContext context)
        {
            _context = context;
            _dbSet = ((DbContext)_context).Set<T>();

            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public IList<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //_dbSet.Include()

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity, string[] Properties = null)
        {
            _dbSet.Attach(entity);

            if (Properties != null)
            {
                ((DbContext)_context).Entry(entity).State = EntityState.Unchanged;
                foreach (string p in Properties)
                {
                    ((DbContext)_context).Entry(entity).Property(p).IsModified = true;
                }
            }
            else
            {
                ((DbContext)_context).Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(T entity)
        {
            if (((DbContext)_context).Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void Save()
        {
            ((DbContext)_context).Configuration.ValidateOnSaveEnabled = false;
            ((DbContext)_context).SaveChanges();
        }

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ((DbContext)_context).Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
