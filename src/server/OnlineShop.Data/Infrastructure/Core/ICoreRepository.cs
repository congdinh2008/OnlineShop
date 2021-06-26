using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineShop.Data.Infrastructure.Core
{
    public interface ICoreRepository<T>
    {
        void Add(T entity);

        void Add(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity, bool isHardDelete = false);

        void Delete(IEnumerable<T> entities, bool isHardDelete = false);

        void Delete(Expression<Func<T, bool>> where, bool isHardDelete = false);

        IQueryable<T> GetQuery();

        IQueryable<T> GetQuery(Expression<Func<T, bool>> where);
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetQueryAsync();
        Task<IEnumerable<T>> GetQueryAsync(Expression<Func<T, bool>> condition);
        IQueryable<T> GetByPage(int size, int page);
        IQueryable<T> GetByPage(Expression<Func<T, bool>> condition, int size, int page);
        Task<IEnumerable<T>> GetByPageAsync(int size, int page);
        Task<IEnumerable<T>> GetByPageAsync(Expression<Func<T, bool>> condition, int size, int page);
    }
}
