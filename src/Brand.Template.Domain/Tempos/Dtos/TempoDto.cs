using System.Text.Json.Serialization;

namespace Brand.Template.Domain.Tempos.Dtos;

public record TempoDto(
    int Id,
    string Descricao,
    TemperaturaDto Temperatura,
    [property: JsonPropertyName("sensacao_termica")] TemperaturaDto SensacaoTermica,
    int Humidade,
    CidadeDto Cidade
);