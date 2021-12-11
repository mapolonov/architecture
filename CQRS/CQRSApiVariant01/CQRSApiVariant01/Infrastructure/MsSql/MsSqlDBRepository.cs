using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CQRSApiVariant01.Infrastructure.MsSql
{

    public abstract class MsSqlDBRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        public DbContext Context { get; }

        public MsSqlDBRepository(DbContext context) => Context = context ?? throw new ArgumentNullException(nameof(context));

        public virtual async Task<T> GetById(int id) => await Context.Set<T>().FindAsync(id);
        public IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public virtual async Task<T> Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Context.Set<T>().Attach(entity);
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        ///     Adds the new entities in the repository.
        /// </summary>
        /// <param name="entities">The entities of type T.</param>
        public virtual async Task Add(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            var toDbList = entities.ToList();

            Context.Set<T>().AttachRange(toDbList);
            await Context.Set<T>().AddRangeAsync(toDbList);
            await Context.SaveChangesAsync();
        }
        
        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType { get; }
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }
    }
}
