using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore.Repository;

namespace ShopManagement.Configuration;

public static class ShopManagementBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        // ثبت سرویس‌ها
        services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
        services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

        // اضافه کردن DbContext یا استفاده از ConnectionString
        services.AddDbContext<ShopContext>(options =>
            options.UseSqlServer(connectionString));
    }
}