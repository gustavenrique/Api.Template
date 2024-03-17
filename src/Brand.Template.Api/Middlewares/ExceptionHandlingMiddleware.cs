using Brand.Template.Api.Filter;

namespace Presentation.Middleware;

internal sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    readonly record struct Error(
        bool Ocurred,
        string Message = "",
        Exception? Exception = null
    );

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Error error = new(Ocurred: false);

        try
        {
            await next(context);
        }
        catch (OperationCanceledException ex)
        {
            error = new Error(true, "Oops! O processo demorou demais para finalizar", ex);
        }
        catch (Exception ex)
        {
            error = new Error(true, "Oops! Algum erro inesperado ocorreu", ex);
        }
        finally
        {
            if (error.Ocurred)
            {
#pragma warning disable CA2254
                _logger.LogError(error.Exception, error.Message);
#pragma warning restore CA2254
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(
                    new Response<object?>(null, [error.Message])
                );
            }
        }
    }
}