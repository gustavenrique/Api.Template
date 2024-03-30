using System.Text.Json.Serialization;

namespace RestApi.Template.Infra.Tempos.Dtos.WeatherResponse;

internal sealed record WeatherResponseDto
{
    [JsonPropertyName("coord")]
    public CoordinatesDto Coordinates { get; init; }
    public WeatherDto[]? Weather { get; init; }
    public MainWeatherDto Main { get; init; }
}