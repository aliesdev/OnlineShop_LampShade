using System.Linq.Expressions;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository;

public class ProductCategoryRepository : BaseRepository<long, ProductCategory>, IProductCategoryRepository
{
    private readonly ShopContext context;

    public ProductCategoryRepository(ShopContext context) : base(context)
    {
        this.context = context;
    }

}