namespace RestApi.Common.Configuration;

/// <summary>
/// Seção individual de um banco SQL no appsettings
/// </summary>
public sealed record SqlOptions(
    string ConnectionString,
    ResilienceOptions Resilience
);