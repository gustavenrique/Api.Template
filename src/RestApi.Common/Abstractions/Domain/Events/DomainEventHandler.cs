using MediatR;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;

namespace RestApi.Common.Abstractions.Domain.Events;

public abstract class DomainEventHandler<TEvent>(ILogger<DomainEventHandler<TEvent>> logger)
    : INotificationHandler<TEvent> where TEvent : IDomainEvent
{
    readonly ILogger _logger = logger;
    readonly AsyncRetryPolicy _retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(
            medianFirstRetryDelay: TimeSpan.FromSeconds(1),
            retryCount: 2
        ));

    public async Task Handle(TEvent @event, CancellationToken cancellationToken)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Evento recebido: {@Input}", @event);
            }

            await _retryPolicy.ExecuteAsync(async () =>
            {
                await Execute(@event, cancellationToken);
            }).ConfigureAwait(false);

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Evento processado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Algo inesperado ocorreu ao lidar com o domain event - Input: {@Event}",
                @event
            );
        }
    }

    protected abstract Task Execute(TEvent @event, CancellationToken cancellationToken);
}