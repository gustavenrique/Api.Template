namespace Brand.Template.Infra.Tempos.Dtos.WeatherResponse;

internal readonly record struct WeatherDto(
    int Id,
    string Main,
    string Description
);