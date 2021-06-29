using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineShop.Services.BaseServices
{
    public interface IBaseService<TEntity>
    {
        int Add(TEntity entity);

        Task<int> AddAsync(TEntity entity);

        int AddRange(IEnumerable<TEntity> entities);

        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

        bool Update(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        bool Delete(Guid id);

        Task<bool> DeleteAsync(Guid id);

        bool DeleteRange(Guid[] ids);

        Task<bool> DeleteRangeAsync(Guid[] ids);

        bool DeleteRange(IEnumerable<TEntity> entities);

        Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);

        TEntity GetById(Guid id);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<Paginated<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10);
    }
}
