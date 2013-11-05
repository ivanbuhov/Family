using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Family.Data.Infrastructure
{
    public interface IRepository<T>
    {
        IQueryable<T> Get(Expression<Func<T, bool>> filter = null);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        void Save();
    }
}
