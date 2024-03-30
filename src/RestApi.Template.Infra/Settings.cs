using RestApi.Common.Configuration;

namespace RestApi.Template.Infra;

/// <summary>
/// Representa as principais configurações de infra do appsettings.json
/// </summary>
public static class Settings
{
    /// <summary>
    /// Seção de 'Database' do appsettings
    /// </summary>
    public sealed class Database
    {
        public required SqlOptions Xpto { get; init; }
        public required SqlOptions Log { get; init; }
    }

    /// <summary>
    /// Seção de 'Api' do appsettings
    /// </summary>
    public sealed class Api
    {
        public required ApiOptions OpenWeather { get; init; }
        public required ApiOptions Elasticsearch { get; init; }
    }
}