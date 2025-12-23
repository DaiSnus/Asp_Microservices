using Core.TraceIdLogic.Interfaces;
using Serilog.Context;

namespace Core.TraceIdLogic;

public class TraceIdMiddleware
{
    private readonly RequestDelegate _next;
    
    public TraceIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITraceReader traceReader, ITraceWriter traceWriter)
    {
        var traceId = context.Request.Headers.TryGetValue("X-Trace-Id", out var values)
            ? values.ToString()
            : null;
        
        traceReader.WriteValue(traceId ?? string.Empty);

        using (LogContext.PushProperty("TraceId", traceWriter.GetValue()))
        {
            context.Response.OnStarting(() =>
            {
                var currentTraceId = traceWriter.GetValue();
                if (!string.IsNullOrEmpty(currentTraceId))
                {
                    context.Response.Headers["X-Trace-Id"] = currentTraceId;
                }
                
                return Task.CompletedTask;
            });
        }
        
        await _next(context);
    }
}