using RestApi.Template.Domain.Tempos.Dtos;
using RestApi.Common.Types.Output;

namespace RestApi.Template.Application.Tempos.Errors;

internal static class CidadeErrors
{
    internal static readonly Result<CidadeDto?> InputInvalido =
        Result<CidadeDto?>.InvalidInput(["Nome da cidade não preenchido corretamente"]);

    internal static readonly Result<CidadeDto?> CidadeInexistente =
        Result<CidadeDto?>.Empty;
}