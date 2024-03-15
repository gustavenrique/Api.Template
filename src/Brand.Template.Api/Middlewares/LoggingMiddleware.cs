using Serilog.Context;

namespace Brand.Template.Api.Middlewares;

internal sealed class LoggingMiddleware : IMiddleware
{
    private static string[] _correlationIdHeaderKeys => ["correlation-id", "x-correlation-id"];

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string correlationId = ExtrairCorrelationId(context.Request.Headers)
            ?? context.TraceIdentifier;

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            return next(context);
        }
    }

    private static string? ExtrairCorrelationId(IHeaderDictionary headers) =>
        headers
            .FirstOrDefault(h => _correlationIdHeaderKeys.Contains(h.Key))
            .Value;
}