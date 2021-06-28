using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Infrastructure.Core;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services.BaseServices
{
    public class BaseServices<TEntity> : IBaseService<TEntity> where TEntity : Entity
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual int Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _unitOfWork.CoreRepository<TEntity>().Add(entity);
            return _unitOfWork.SaveChanges();
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _unitOfWork.CoreRepository<TEntity>().Add(entity);
            return await _unitOfWork.SaveChangesAsync();
        }

        public int AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                _unitOfWork.CoreRepository<TEntity>().Add(item);
            }
            return _unitOfWork.SaveChanges();

        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                _unitOfWork.CoreRepository<TEntity>().Add(item);
            }
            return await _unitOfWork.SaveChangesAsync();
        }

        public bool Delete(Guid id)
        {
            var entity = _unitOfWork.CoreRepository<TEntity>().GetById(id);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _unitOfWork.CoreRepository<TEntity>().Delete(entity);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = _unitOfWork.CoreRepository<TEntity>().GetById(id);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _unitOfWork.CoreRepository<TEntity>().Delete(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public bool DeleteRange(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRangeAsync(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<Paginated<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "", int pageIndex = 1, int pageSize = 10)
        {
            var query = _unitOfWork.CoreRepository<TEntity>().Get(filter: filter, orderBy: orderBy, 
                includeProperties: includeProperties);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await Paginated<TEntity>.CreateAsync(query.AsNoTracking(), pageIndex, pageSize);
        }

        public TEntity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual bool Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _unitOfWork.CoreRepository<TEntity>().Update(entity);
            return _unitOfWork.SaveChanges() > 0;
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
