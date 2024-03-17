using Brand.Template.Domain.Tempos.Dtos;
using Brand.Template.Domain.Tempos.Models;
using Brand.Template.Domain.Tempos.Models.ValueObjects;

namespace Brand.Template.Application.Tempos;
internal static class TempoMapper
{
    /// <summary>
    /// De DTO para Brand.Template.Domain object
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static Tempo ParaDomain(this TempoDto dto)
    {
        var temperatura = Temperatura.Criar(dto.Temperatura.Celsius);
        var sensacaoTermica = Temperatura.Criar(dto.SensacaoTermica.Celsius);

        Cidade cidade = new Cidade(
            new CidadeId(dto.Cidade.Id.Latitude, dto.Cidade.Id.Longitude),
            dto.Cidade.Nome
        );

        return new Tempo(
            dto.Id,
            dto.Descricao,
            temperatura.Value!,
            sensacaoTermica.Value!,
            dto.Humidade,
            cidade
        );
    }
}