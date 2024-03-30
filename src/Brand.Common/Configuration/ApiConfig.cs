namespace Brand.Common.Configuration;

/// <summary>
/// Seção individual de uma API no appsettings
/// </summary>
public sealed class ApiConfig
{
    /// <summary>
    /// Base URL
    /// </summary>
    public required string Url { get; init; }

    /// <summary>
    /// Token de autenticação
    /// </summary>
    public string Token { get; init; } = string.Empty;

    /// <summary>
    /// Timeout de requisição em segundos
    /// </summary>
    public required int Timeout { get; init; }

    public required ResilienceConfig Resilience { get; init; }
}