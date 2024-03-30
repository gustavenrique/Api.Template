using Polly.Contrib.WaitAndRetry;

namespace RestApi.Common.Configuration;

/// <summary>
/// Principais parâmetros de <see cref="Backoff.DecorrelatedJitterBackoffV2" />
/// </summary>
public sealed class ResilienceOptions
{
    public required double MedianFirstRetryDelay { get; init; }
    public required int RetryCount { get; init; }
}