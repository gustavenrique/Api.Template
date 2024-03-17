using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedKernel.Abstractions.ResultType;
using SharedKernel.ResultType;

namespace Brand.Template.Api.Filter;

internal sealed class ResponseMappingFilter(
    IMapper mapper,
    ILogger<ResponseMappingFilter> logger
) : IActionFilter
{
    readonly IMapper _mapper = mapper;
    readonly ILogger _logger = logger;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is null)
        {
            var response = context.Result as ObjectResult;
            object? resultObject = response?.Value;

            if (resultObject is not Result)
            {
                _logger.LogError("Valor retornado não é um Result type - Resultado: {@Resultado}", response);

                throw new InvalidCastException("O tipo de retorno deve ser um Result");
            }

            response!.StatusCode = DeterminarStatusCode((Result)resultObject);

            if (response.StatusCode is not StatusCodes.Status204NoContent)
            {
                response.Value = _mapper.Map<Response<object>>(resultObject);
            }
        }
    }

    private static int DeterminarStatusCode(Result resultObject) =>
        resultObject.Reason switch
        {
            ResultReason.Ok => StatusCodes.Status200OK,
            ResultReason.Created => StatusCodes.Status201Created,
            ResultReason.Empty => StatusCodes.Status204NoContent,
            ResultReason.InvalidInput => StatusCodes.Status400BadRequest,
            ResultReason.UnexistentId => StatusCodes.Status404NotFound,
            ResultReason.BusinessLogicViolation => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError,
        };
}