namespace RestApi.Common.Configuration;

public sealed class AuthOptions
{
    /// <summary>
    /// Habilita a api-key based authentication
    /// </summary>
    public required bool Enabled { get; init; }

    /// <summary>
    /// Headers onde procurar a api-key
    /// </summary>
    public required string[] Headers { get; init; }

    /// <summary>
    /// API keys válidas
    /// </summary>
    public required Dictionary<string, string> ApiKeys { get; init; }
}