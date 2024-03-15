using System.Text.Json.Serialization;

namespace Brand.Template.Infra.Tempos.Dtos.WeatherResponse;

internal readonly record struct MainWeatherDto
{
    [JsonPropertyName("temp")]
    public decimal Temperature { get; init; }

    [JsonPropertyName("feels_like")]
    public decimal FeelsLike { get; init; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; init; }
}