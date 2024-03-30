using Microsoft.Extensions.Options;
using RestApi.Common.Configuration;
using RestApi.Template.Api.Filters.ResponseMapping;

namespace Presentation.Middleware;

internal sealed class AuthMiddleware(
    IOptionsMonitor<AuthOptions> securitySettings
) : IMiddleware
{
    readonly AuthOptions _auth = securitySettings.CurrentValue;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        bool apiPath = context.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase);

        if (_auth.Enabled && apiPath)
        {
            string? apiKey = ExtrairApiKey(context.Request.Headers);

            if (string.IsNullOrEmpty(apiKey) || !_auth.ApiKeys.ContainsValue(apiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                Response<object?> body = new(null, ["Preencha o api-key header"]);

                await context.Response.WriteAsJsonAsync(body);

                return;
            }
        }

        await next(context);
    }

    private string? ExtrairApiKey(IHeaderDictionary headers) =>
        headers
            .FirstOrDefault(h => _auth.Headers.Contains(h.Key.ToLowerInvariant()))
            .Value;
}