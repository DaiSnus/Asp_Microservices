
using Core.HttpLogic;
using Core.TraceIdLogic;
using Microsoft.EntityFrameworkCore;
using OrderService.Application;
using OrderService.Infrastructure;
using OrderService.Infrastructure.DataLayer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration["OrderDb"];

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.TryAddTraceId();
builder.Services.AddHttpRequestService();

builder.Services.AddHttpClient("product-api", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ProductApi:ProductApi"]!);
}).AddHttpMessageHandler<TraceIdHeaderHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfrastructure(conn!);
builder.Services.AddApplication();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseMiddleware<TraceIdMiddleware>();
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();