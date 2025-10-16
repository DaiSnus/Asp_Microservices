using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Repository.Interfaces;
using OrderService.Infrastructure.DataLayer;
using OrderService.Infrastructure.Repository.Implementations;

namespace NvkBerries.OrderService.OrderService.Infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(connectionString));
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}