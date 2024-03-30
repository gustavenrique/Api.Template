using RestApi.Template.Domain.Tempos.Dtos;
using RestApi.Common.Types.Output;

namespace RestApi.Template.Application.Tempos.Errors;

internal static class TempoErrors
{
    internal static readonly Result<TempoDto?> CidadeInvalida =
        Result<TempoDto?>.InvalidInput(["Determine a cidade cujo clima será buscado"]);

    internal static readonly Result<TempoDto?> TempoNaoEncontrado =
        Result<TempoDto?>.UnexpectedError(["Algum erro ocorreu ao tentar buscar o tempo"]);
}