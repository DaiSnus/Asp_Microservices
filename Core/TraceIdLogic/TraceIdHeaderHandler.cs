using Core.TraceIdLogic.Interfaces;

namespace Core.TraceIdLogic;

public class TraceIdHeaderHandler : DelegatingHandler
{
    private readonly ITraceWriter  _traceWriter;
    public TraceIdHeaderHandler(ITraceWriter traceWriter) => _traceWriter = traceWriter;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var traceId = _traceWriter.GetValue();

        if (!string.IsNullOrEmpty(traceId))
        {
            if (!request.Headers.Contains("X-Trace-Id"))
                request.Headers.Add("X-Trace-Id", traceId);
        }
        
        return await base.SendAsync(request, cancellationToken);
    }
}