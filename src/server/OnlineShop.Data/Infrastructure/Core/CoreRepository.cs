using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineShop.Data.Infrastructure.Core
{
    public class CoreRepository<T> : ICoreRepository<T> where T : class, IEntity
    {
        #region Protected Fields

        protected readonly OnlineShopDbContext DataContext;
        protected readonly DbSet<T> DbSet;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T, TDbContext}"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public CoreRepository(OnlineShopDbContext dataContext)
        {
            this.DataContext = dataContext;

            // Find Property with typeof(T) on dataContext
            var typeOfDbSet = typeof(DbSet<T>);
            foreach (var prop in dataContext.GetType().GetProperties())
            {
                if (typeOfDbSet == prop.PropertyType)
                {
                    DbSet = prop.GetValue(dataContext, null) as DbSet<T>;
                    break;
                }
            }
            DbSet ??= dataContext.Set<T>();
        }

        #region Virtual Methods

        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Add(IEnumerable<T> entities)
        {
            // Use AddRange() to improve the performance.
            DbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }

        /// <summary>
        /// Deletes the specified context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isHardDelete">is hard delete?</param>
        public virtual void Delete(T entity, bool isHardDelete = false)
        {
            if (isHardDelete)
            {
                DbSet.Remove(entity);
            }
            else
            {
                entity.IsDeleted = true;
                DbSet.Update(entity);
            }
        }

        /// <summary>
        /// Deletes the specified context.
        /// </summary>
        /// <param name="entities">List of entity.</param>
        /// <param name="isHardDelete">is hard delete?</param>
        public virtual void Delete(IEnumerable<T> entities, bool isHardDelete = false)
        {
            // Improve performance for hard delete
            if (isHardDelete)
                DbSet.RemoveRange(entities);
            else
                foreach (var entity in entities)
                {
                    entity.IsDeleted = true;
                }
        }

        /// <summary>
        /// Deletes the specified context.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="isHardDelete">is hard delete?</param>
        public virtual void Delete(Expression<Func<T, bool>> where, bool isHardDelete = false)
        {
            var entities = GetQuery(where).AsEnumerable();

            // Use this overload instead of using foreach to improve performance
            Delete(entities, isHardDelete);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual IQueryable<T> GetQuery()
        {
            return DbSet;
        }

        public virtual async Task<IEnumerable<T>> GetByPageAsync(Expression<Func<T, bool>> condition, int size, int page)
        {
            return await DbSet.Where(condition).Skip(size * (page - 1)).Take(size).ToListAsync();
        }

        // x=>x.Name.Contains(searchString)
        // OrderBy(x=>x.Name);
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // includeProperties = "Products"
            foreach (var includeProperty in
                includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? orderBy(query) : query;
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where);
        }

        #endregion
    }
}
