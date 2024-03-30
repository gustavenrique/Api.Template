using Brand.Template.Domain.Tempos.Dtos;
using Brand.Common.Types.Output;

namespace Brand.Template.Application.Tempos.Errors;

internal static class CidadeErrors
{
    internal static readonly Result<CidadeDto?> InputInvalido =
        Result<CidadeDto?>.InvalidInput(["Nome da cidade não preenchido corretamente"]);

    internal static readonly Result<CidadeDto?> CidadeInexistente =
        Result<CidadeDto?>.Empty;
}