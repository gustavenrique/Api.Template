using Polly.Contrib.WaitAndRetry;

namespace Brand.Common.Configuration;

/// <summary>
/// Principais parâmetros de <see cref="Backoff.DecorrelatedJitterBackoffV2" />
/// </summary>
public sealed record ResilienceConfig(
    double MedianFirstRetryDelay,
    int RetryCount
);