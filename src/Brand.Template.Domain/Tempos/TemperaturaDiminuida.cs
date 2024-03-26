using System.Text.Json.Serialization;
using Brand.SharedKernel.Abstractions.Events;

namespace Brand.Template.Domain.Tempos;

public sealed record TemperaturaDiminuida(
    [property: JsonPropertyName("tempo_id")]
    int TempoId,

    [property: JsonPropertyName("temperatura_antiga")]
    decimal TemperaturaAntiga,

    [property: JsonPropertyName("temperatura_atual")]
    decimal TemperaturaAtual
) : IDomainEvent;