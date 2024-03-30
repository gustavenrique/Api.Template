namespace RestApi.Common.Configuration;

/// <summary>
/// Seção individual de um banco SQL no appsettings
/// </summary>
public sealed class SqlOptions
{
    public required string ConnectionString { get; init; }
    public required ResilienceOptions Resilience { get; init; } = default!;
}