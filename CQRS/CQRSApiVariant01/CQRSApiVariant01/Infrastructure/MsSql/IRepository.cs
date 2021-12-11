using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSApiVariant01.Infrastructure.MsSql
{
    public interface IRepository<T> : IQueryable<T>
        where T : IEntity
    {
        Task<T> GetById(int id);

        IQueryable<T> GetAll();

        Task<T> Add(T entity);

        Task Add(IEnumerable<T> entities);

        bool Exists(Expression<Func<T, bool>> predicate);
    }
}
