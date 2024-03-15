using System.Text.Json.Serialization;

namespace Brand.Template.Infra.Tempos.Dtos;

internal readonly record struct CoordinatesDto
{
    [JsonPropertyName("lat")]
    public decimal Latitude { get; init; }

    [JsonPropertyName("lon")]
    public decimal Longitude { get; init; }
};