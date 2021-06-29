using OnlineShop.Data.Infrastructure.Core;
using OnlineShop.Models;
using OnlineShop.Services.BaseServices;

namespace OnlineShop.Services
{
    public class CategoryServices : BaseServices<Category>, ICategoryService
    {
        public CategoryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
