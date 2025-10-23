
using Microsoft.EntityFrameworkCore;
using ProductService.Dal;
using ProductService.Dal.Data;
using ProductService.Logic.Services.Implementations;
using ProductService.Logic.Services.Inerfaces;

namespace ProductService.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddPsqlLayer(builder.Configuration.GetConnectionString("default")!);

        builder.Services.AddScoped<IProductStatisticService, ProductStatisticService>();
        builder.Services.AddScoped<IProductService, Logic.Services.Implementations.ProductService>();
        
        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            await dbContext.Database.MigrateAsync();
        }
        
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        
        app.MapControllers();
        await app.RunAsync();
    }
}
