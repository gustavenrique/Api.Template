namespace RestApi.Common.Configuration;

/// <summary>
/// Seção individual de um banco SQL no appsettings
/// </summary>
public sealed record SqlConfig(
    string ConnectionString,
    ResilienceConfig Resilience
);