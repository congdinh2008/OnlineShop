using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineShop.Data.Infrastructure.Core
{
    public class CoreRepository<T> : ICoreRepository<T> where T : Entity
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
                DbSet.Remove(entity);
            else
                entity.IsDeleted = true;
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
            // Remove AsQueryable() since DbSet<T> already implemented IQueryable<T>
            return DbSet;
        }

        public virtual IQueryable<T> GetQuery(Expression<Func<T, bool>> condition)
        {
            return DbSet.Where(condition);
        }

        public virtual async Task<IEnumerable<T>> GetQueryAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetQueryAsync(Expression<Func<T, bool>> condition)
        {
            return await DbSet.Where(condition).ToListAsync();
        }

        public virtual IQueryable<T> GetByPage(int size, int page)
        {
            return DbSet.Skip(size * (page - 1)).Take(size);
        }

        public virtual IQueryable<T> GetByPage(Expression<Func<T, bool>> condition, int size, int page)
        {
            return DbSet.Where(condition).Skip(size * (page - 1)).Take(size);
        }

        public virtual async Task<IEnumerable<T>> GetByPageAsync(int size, int page)
        {
            return await DbSet.Skip(size * (page - 1)).Take(size).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetByPageAsync(Expression<Func<T, bool>> condition, int size, int page)
        {
            return await DbSet.Where(condition).Skip(size * (page - 1)).Take(size).ToListAsync();
        }

        #endregion
    }
}
