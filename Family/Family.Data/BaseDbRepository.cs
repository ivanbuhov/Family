using Family.Data.Infrastructure;
using Family.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Family.Data
{
    public abstract class BaseDbRepository<T> : IRepository<T>, IDisposable where T : class
    {
        protected DbContext db;

        public BaseDbRepository(DbContext context)
        {
            if (context == null)
            {
                throw new NullReferenceException("The database context is not specified.");
            }

            this.db = context;
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> result = this.db.Set<T>();
            if (filter != null)
            {
                result = result.Where(filter);
            }

            return result;
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("Not predicate specified.");
            }

            return this.db.Set<T>().SingleOrDefault(predicate);
        }

        public virtual void Insert(T entity)
        {
            this.db.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry entityEntry = this.db.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                this.db.Set<T>().Attach(entity);
                entityEntry.State = EntityState.Modified;
            }
        }

        public virtual void Delete(T entity)
        {
            this.db.Set<T>().Remove(entity);
        }

        public virtual void Save()
        {
            this.db.SaveChanges();
        }

        public virtual void Dispose()
        {
            this.db.Dispose();
        }
    }
}
