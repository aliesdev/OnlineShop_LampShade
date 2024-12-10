using _01_Framework.Domain;

namespace ShopManagement.Domain.ProductCategoryAgg;

public interface IProductCategoryRepository : IRepository<long, ProductCategory>
{
}