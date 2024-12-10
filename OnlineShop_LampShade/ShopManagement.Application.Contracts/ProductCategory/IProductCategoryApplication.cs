using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductCategory;

public interface IProductCategoryApplication
{
    OperationResult Create(CreateProductCategory command);
    OperationResult Edit(EditProductCategory command);
    List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    Domain.ProductCategoryAgg.ProductCategory Get(long id);
    EditProductCategory GetDetails(long id);

}