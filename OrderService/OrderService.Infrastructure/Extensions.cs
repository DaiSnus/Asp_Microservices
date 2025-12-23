using Microsoft.EntityFrameworkCore;
using OrderService.Application.Clients.Interfaces;
using OrderService.Domain.Repository.Interfaces;
using OrderService.Infrastructure.Clients.Implementations;
using OrderService.Infrastructure.DataLayer;
using OrderService.Infrastructure.Repository.Implementations;

namespace OrderService.Infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(connectionString));
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductApiClient, ProductApiClient>();
    }
}