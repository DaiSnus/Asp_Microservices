using OrderService.Application.Services.Interfaces;

namespace OrderService.Application;

public static class Extensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, Services.Implementations.OrderService>();
    }
}