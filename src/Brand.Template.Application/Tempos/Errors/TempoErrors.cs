using Brand.SharedKernel.Types.Output;
using Brand.Template.Domain.Tempos.Dtos;

namespace Brand.Template.Application.Tempos.Errors;

internal static class TempoErrors
{
    internal static readonly Result<TempoDto?> CidadeInvalida =
        Result<TempoDto?>.InvalidInput(["Determine a cidade cujo clima será buscado"]);

    internal static readonly Result<TempoDto?> TempoNaoEncontrado =
        Result<TempoDto?>.UnexpectedError(["Algum erro ocorreu ao tentar buscar o tempo"]);
}