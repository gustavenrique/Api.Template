namespace Brand.Common.Types;

/// <summary>
/// Representação de input, principalmente para métodos de services
/// </summary>
/// <remarks>
/// Inspirado no <see href="https://refactoring.guru/introduce-parameter-object">Parameter Object Pattern</see>
/// </remarks>
public record Input<TValue>
{
    /// <summary>
    /// Parãmetro(s) principal(is)
    /// </summary>
    public TValue Value { get; init; } = default!;
    public CancellationToken CancellationToken { get; init; } = CancellationToken.None;
    public string CorrelationId { get; init; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Para armazenar outros dados além dos acima
    /// </summary>
    public IReadOnlyDictionary<string, object>? AdditionalData { get; init; }
}