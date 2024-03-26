using Brand.SharedKernel.Abstractions.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Brand.SharedKernel.Services;

internal sealed class DomainEventDispatcher(ILogger<DomainEventDispatcher> logger, IPublisher publisher) : IDomainEventDispatcher
{
    private readonly ILogger<DomainEventDispatcher> _logger = logger;
    private readonly IPublisher _publisher = publisher;

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
                    await _publisher.Publish(@event);
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