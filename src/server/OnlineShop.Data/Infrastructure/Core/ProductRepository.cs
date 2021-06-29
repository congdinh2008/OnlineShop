using OnlineShop.Models;

namespace OnlineShop.Data.Infrastructure.Core
{
    public class ProductRepository : CoreRepository<Product>, IProductRepository
    {
        public ProductRepository(OnlineShopDbContext dataContext) : base(dataContext)
        {
        }
    }
}