using OnlineShop.Models;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Data.Infrastructure.Core
{
    public interface IUnitOfWork : IDisposable
    {
        OnlineShopDbContext DataContext { get; }

        #region Master Data

        ICoreRepository<Category> CategoryRepository { get; }

        IProductRepository ProductRepository { get; }

        #endregion

        int SaveChanges();

        Task<int> SaveChangesAsync();

        ICoreRepository<T> CoreRepository<T>() where T : Entity;
    }
}
