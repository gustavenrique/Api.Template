using Brand.Template.Domain.Tempos.Dtos;
using SharedKernel.ResultType;

namespace Brand.Template.Application.Tempos.Errors;

internal static class CidadeErrors
{
    internal static readonly Result<CidadeDto?> CidadeInvalida =
        Result<CidadeDto?>.InvalidInput(["Nome da cidade não preenchido corretamente"]);

    internal static readonly Result<CidadeDto?> CidadeNaoEncontrada =
        Result<CidadeDto?>.Empty();
}