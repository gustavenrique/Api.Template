using System.Text.Json.Serialization;
using RestApi.Common.Abstractions.Domain.Events;

namespace RestApi.Template.Domain.Tempos;

public sealed record TemperaturaDiminuida(
    [property: JsonPropertyName("tempo_id")]
    int TempoId,

    [property: JsonPropertyName("temperatura_antiga")]
    decimal TemperaturaAntiga,

    [property: JsonPropertyName("temperatura_atual")]
    decimal TemperaturaAtual
) : IDomainEvent;