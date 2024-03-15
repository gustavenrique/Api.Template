﻿using System.Text.Json.Serialization;
using SharedKernel.Abstractions.Events;

namespace Brand.Template.Domain.Tempos;

public sealed record TemperaturaDiminuidaEvent(
    [property: JsonPropertyName("tempo_id")]
    int TempoId,

    [property: JsonPropertyName("temperatura_antiga")]
    decimal TemperaturaAntiga,

    [property: JsonPropertyName("temperatura_atual")]
    decimal TemperaturaAtual
) : IEvent;