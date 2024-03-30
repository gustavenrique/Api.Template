namespace RestApi.Common.Configuration;

public sealed class KeyVaultOptions
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