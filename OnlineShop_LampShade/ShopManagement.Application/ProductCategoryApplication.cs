using System.Globalization;
using _01_Framework.Application;
using _01_Framework.Domain;
using Microsoft.VisualBasic;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application;

public class ProductCategoryApplication : IProductCategoryApplication
{
    private readonly IProductCategoryRepository productCategoryRepository;

    public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
    {
        this.productCategoryRepository = productCategoryRepository;
    }

    public OperationResult Create(CreateProductCategory command)
    {
        var operation = new OperationResult();
        if (productCategoryRepository.Exists(x => x.Name == command.Name))
        {
            return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد");
        }

        // var slug = command.Slug.Slugify();
        var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
            command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,
            command.Slug.Slugify());
        productCategoryRepository.Create(productCategory);
        productCategoryRepository.SaveChanges();
        return operation.Succeeded();
    }

    public OperationResult Edit(EditProductCategory command)
    {
        var operation = new OperationResult();
        var productCategory = productCategoryRepository.Get(command.Id);

        if (productCategory == null)
            return operation.Failed("چنین موردی با این آیدی وجود ندارد");

        if (productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            return operation.Failed("موردی با این نام از قبل وجود دارد");

        productCategory.Edit(command.Name, command.Description, command.Picture,
            command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,
            command.Slug.Slugify());
        productCategoryRepository.SaveChanges();

        return operation.Succeeded();
    }

    public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
    {
        IEnumerable<ProductCategory> result = string.IsNullOrEmpty(searchModel?.Name)
            ? productCategoryRepository.GetAll()
            : productCategoryRepository.Search(pc => pc.Name.Contains(searchModel.Name));

        return result.Select(entity => new ProductCategoryViewModel
        {
            Id = entity.Id,
            Name = entity.Name,
            CreationDate = entity.CreationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            Picture = entity.Picture,
            // ProductsCount = entity.Products?.Count ?? 0 // Safely handling null Products
        }).ToList();
    }

    public ProductCategory Get(long id)
    {
        return productCategoryRepository.Get(id);
    }

    public EditProductCategory GetDetails(long id)
    {
        var entity = productCategoryRepository.GetDetails(id);
        if (entity == null) return null;
        return new EditProductCategory
        {
            Id = entity.Id, Name = entity.Name, Description = entity.Description, Picture = entity.Picture,
            PictureAlt = entity.PictureAlt, PictureTitle = entity.PictureTitle, Keywords = entity.Keywords,
            MetaDescription = entity.MetaDescription, Slug = entity.Slug
        };
    }
}