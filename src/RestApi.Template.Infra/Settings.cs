using Polly.Contrib.WaitAndRetry;

namespace RestApi.Template.Infra;

/// <summary>
/// Representa as principais configurações de infra do appsettings.json
/// </summary>
public static class Settings
{
    /// <summary>
    /// Seção de KeyVault do appsettings global
    /// </summary>
    public sealed class KeyVault
    {
        /// <summary>
        /// Com a aplicação rodando no Azure, apenas a URL do Key Vault é requerida,
        /// pois pode ser utilizado o Managed Identity para autenticação automática
        /// </summary>
        public required string Url { get; init; }

        /* 
         * As três props abaixo servem apenas para desenvolvimento local
         * e devem ser setadas via .NET Secret Manager
        */
        public string? TenantId { get; init; }
        public string? ClientId { get; init; }
        public string? ClientSecret { get; init; }
    }

    /// <summary>
    /// Seção de 'Database' do appsettings
    /// </summary>
    public sealed class Database
    {
        public required Options.Sql Xpto { get; init; }
        public required Options.Sql Log { get; init; }
    }

    /// <summary>
    /// Seção de 'Api' do appsettings
    /// </summary>
    public sealed class Api
    {
        public required Options.Api OpenWeather { get; init; }
        public required Options.Api Elasticsearch { get; init; }
    }
}

/// <summary>
/// Contém classes que representam seções específicas de configuração
/// </summary>
public static class Options
{
    /// <summary>
    /// Seção individual de um banco SQL
    /// </summary>
    public sealed class Sql
    {
        public required string ConnectionString { get; init; }

        public required Resilience Resilience { get; init; }
    }

    /// <summary>
    /// Seção individual de uma API
    /// </summary>
    public sealed class Api
    {
        /// <summary>
        /// Base URL
        /// </summary>
        public required string Url { get; init; }

        /// <summary>
        /// Api-key token, if needed
        /// </summary>
        public string Token { get; init; } = string.Empty;

        /// <summary>
        /// Timeout de requisição em segundos
        /// </summary>
        public required int Timeout { get; init; }

        public required Resilience Resilience { get; init; }
    }

    /// <summary>
    /// Principais parâmetros de <see cref="Backoff.DecorrelatedJitterBackoffV2" />
    /// </summary>
    public sealed class Resilience
    {
        public double MedianFirstRetryDelay { get; init; }

        public int RetryCount { get; init; }
    }
}