using System.Text.Json.Serialization;

namespace RestApi.Template.Infra.Tempos.Dtos;

internal readonly record struct CoordinatesDto
{
    [JsonPropertyName("lat")]
    public decimal Latitude { get; init; }

    [JsonPropertyName("lon")]
    public decimal Longitude { get; init; }
};