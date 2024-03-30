using Brand.Common.Abstractions.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Brand.Common.Services;

internal sealed class DomainEventDispatcher(
    ILogger<DomainEventDispatcher> logger,
    IPublisher publisher
) : IDomainEventDispatcher
{
    readonly ILogger<DomainEventDispatcher> _logger = logger;
    readonly IPublisher _publisher = publisher;

    public void Dispatch(IDomainEvent @event) => Dispatch([@event]);

    public void Dispatch(IReadOnlyCollection<IDomainEvent> events)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Iniciando disparo de domain events");
        }

        Task.Run(async () =>
        {
            try
            {
                foreach (var @event in events)
                {
                    await _publisher
                        .Publish(@event)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Não foi possível publicar os domain events - Events: {@Events}",
                    events
                );
            }
        });
    }
}