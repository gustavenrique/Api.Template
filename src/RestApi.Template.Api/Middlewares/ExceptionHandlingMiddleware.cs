using RestApi.Template.Api.Filters.ResponseMapping;

namespace Presentation.Middleware;

internal sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    sealed record Error(string Message, Exception Exception);

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Error? error = null;

        try
        {
            await next(context);
        }
        catch (OperationCanceledException ex)
        {
            error = new Error("Oops! O processo demorou demais para finalizar", ex);
        }
        catch (Exception ex)
        {
            error = new Error("Oops! Algum erro inesperado ocorreu", ex);
        }
        finally
        {
            if (error is not null)
            {
                _logger.LogError(error.Exception, "{Error}", error.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(
                    new Response<object?>(null, [error.Message])
                );
            }
        }
    }
}