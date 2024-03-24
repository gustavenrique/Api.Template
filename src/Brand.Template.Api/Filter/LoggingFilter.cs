using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog.Context;

namespace Brand.Template.Api.Filter;

internal sealed class LoggingFilter(ILogger<LoggingFilter> logger) : IAsyncActionFilter
{
    readonly static string[] s_correlationIdHeaderKeys = ["correlation-id", "x-correlation-id"];
    readonly ILogger<LoggingFilter> _logger = logger;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        HttpContext httpContext = context.HttpContext;

        string correlationId = ExtrairCorrelationId(httpContext.Request.Headers)
            ?? httpContext.TraceIdentifier;

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(
                    "Endpoint: {HttpMethod} {Path} - Action Method: {Action} - Inputs: {@ActionArguments}",
                    httpContext.Request.Method,
                    httpContext.Request.Path,
                    context.ActionDescriptor.DisplayName,
                    context.ActionArguments
                );
            }

            var actionResult = await next();

            if (_logger.IsEnabled(LogLevel.Information))
            {
                var result = actionResult.Result as ObjectResult;

                _logger.LogInformation(
                    "Result: {@Result} - Status Code: {StatusCode}",
                    result?.Value,
                    context.HttpContext.Response.StatusCode
                );
            }
        }
    }

    private static string? ExtrairCorrelationId(IHeaderDictionary headers) =>
        headers
            .FirstOrDefault(h => s_correlationIdHeaderKeys.Contains(h.Key))
            .Value;
}

