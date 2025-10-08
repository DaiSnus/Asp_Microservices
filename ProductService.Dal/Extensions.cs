using Microsoft.EntityFrameworkCore;
using ProductService.Dal.Data;
using ProductService.Dal.Repository.Implementations;
using ProductService.Dal.Repository.Interfaces;

namespace ProductService.Dal;

public static class Extensions
{
    public static IServiceCollection AddPsqlLayer(this IServiceCollection services)
    {
        services.AddDbContext<ProductDbContext>(o => o.UseNpgsql("default"));
        
        services.AddScoped<IProductMediaRepository, ProductMediaRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductStatisticRepository, ProductStatisticRepository>();
        
        return services;
    }
}