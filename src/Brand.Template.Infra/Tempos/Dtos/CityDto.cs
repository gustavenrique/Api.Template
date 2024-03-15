using System.Text.Json.Serialization;

namespace Brand.Template.Infra.Tempos.Dtos;

internal readonly record struct CityDto
{
    public string Name { get; init; }
    public string Country { get; init; }
    public string State { get; init; }

    [JsonPropertyName("lat")]
    public decimal Latitude { get; init; }

    [JsonPropertyName("lon")]
    public decimal Longitude { get; init; }
}