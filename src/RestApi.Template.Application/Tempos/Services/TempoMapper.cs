using RestApi.Template.Domain.Tempos.Dtos;
using RestApi.Template.Domain.Tempos.Models;
using RestApi.Template.Domain.Tempos.Models.ValueObjects;
using RestApi.Common.Types.Output;

namespace RestApi.Template.Application.Tempos.Services;

internal static class TempoMapper
{
    /// <summary>
    /// De DTO para Domain object
    /// </summary>
    public static Result<Tempo?> ParaDomain(this TempoDto dto)
    {
        Result<Temperatura?> temperatura = Temperatura.Criar(dto.Temperatura.Celsius);

        if (temperatura.IsFailure)
        {
            return new(temperatura);
        }

        Result<Temperatura?> sensacaoTermica = Temperatura.Criar(dto.SensacaoTermica.Celsius);

        if (sensacaoTermica.IsFailure)
        {
            return new(sensacaoTermica);
        }

        Cidade cidade = new(
            new CidadeId(dto.Cidade.Id.Latitude, dto.Cidade.Id.Longitude),
            dto.Cidade.Nome
        );

        Tempo tempo = new(
            dto.Id,
            dto.Descricao,
            temperatura.Value!,
            sensacaoTermica.Value!,
            dto.Humidade,
            cidade
        );

        return Result<Tempo?>.Ok(tempo);
    }
}