using Core.TraceIdLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog.Context;

namespace Core.TraceIdLogic;

public interface ITraceIdAccessor
{
    
}

public static class StartUpTraceId
{
    public static IServiceCollection TryAddTraceId(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<TraceIdAccessor>();
        serviceCollection
            .TryAddScoped<ITraceWriter>(provider => provider.GetRequiredService<TraceIdAccessor>());
        serviceCollection
            .TryAddScoped<ITraceReader>(provider => provider.GetRequiredService<TraceIdAccessor>());
        serviceCollection
            .TryAddScoped<ITraceIdAccessor>(provider => provider.GetRequiredService<TraceIdAccessor>());

        serviceCollection.AddTransient<TraceIdHeaderHandler>();
        
        return serviceCollection;
    }
}

internal class TraceIdAccessor : ITraceIdAccessor, ITraceReader, ITraceWriter
{
    public string Name => "TraceId";
    
    private string _value;
    
    public string GetValue()
    {
        return _value;
    }

    public void WriteValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            value = Guid.NewGuid().ToString();
        }
        
        _value = value;
        LogContext.PushProperty("TraceId", value);
    }
}