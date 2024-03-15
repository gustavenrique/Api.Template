using Brand.Template.Infra.Tempos.Dtos;
using Brand.Template.Infra.Tempos.Dtos.WeatherResponse;

using Refit;

namespace Brand.Template.Infra.Tempos;

internal interface IOpenWeatherApiClient
{
    /// <summary>
    /// Busca cidades do mundo que possuem o nome especificado
    /// </summary>
    /// <param name="apiToken"></param>
    /// <param name="cidade">Nome da cidade</param>
    [Get("/geo/1.0/direct")]
    public Task<CityDto[]> BuscarCidades(
        [AliasAs("appid")] string apiToken,
        [AliasAs("q")] string cidade
    );

    /// <summary>
    /// Busca o tempo do local informado por coordenadas
    /// </summary>
    /// <param name="apiToken">Token de segurança</param>
    /// <param name="latitude">Latitude do local</param>
    /// <param name="longitude">Longitude do local</param>
    /// <param name="temperaturaUnidadeMedida">
    ///     Unidade de medida da temperatura. Por padrão, é Kelvin
    ///     Opções: 
    ///     - metric = Celsius
    ///     - imperial = Fahrenheit
    /// </param>
    /// <param name="idioma">pt | en</param>
    [Get("/data/2.5/weather")]
    public Task<WeatherResponseDto?> BuscarTempoLocal(
        [AliasAs("appid")] string apiToken,
        [AliasAs("lat")] decimal latitude,
        [AliasAs("lon")] decimal longitude,
        [AliasAs("units")] string temperaturaUnidadeMedida = "metric",
        [AliasAs("lang")] string idioma = "pt"
    );
}