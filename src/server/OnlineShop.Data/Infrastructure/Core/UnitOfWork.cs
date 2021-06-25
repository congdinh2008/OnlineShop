using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Infrastructure.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineShopDbContext _dbContext;

        public OnlineShopDbContext DataContext => _dbContext;

        public UnitOfWork(OnlineShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        private ICoreRepository<Category> _categoryRepository;

        public ICoreRepository<Category> CategoryRepository =>
            _categoryRepository ??= new CoreRepository<Category>(_dbContext);

        #region Methods
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();

        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
       
        public void SetDetachChanges(bool value)
        {
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = value;
        }

        public ICoreRepository<T> CoreRepository<T>() where T : Entity
        {
            return new CoreRepository<T>(_dbContext);
        }

        public void Dispose()
        {
            this._dbContext.Dispose();
        }
        #endregion
    }
}
