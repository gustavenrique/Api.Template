using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Abstractions.Events;

namespace SharedKernel.Services;

internal sealed class EventDispatcher(ILogger<EventDispatcher> logger, IPublisher publisher) : IEventDispatcher
{
    private readonly ILogger<EventDispatcher> _logger = logger;
    private readonly IPublisher _publisher = publisher;

    public void Dispatch(IEvent @event)
    {
        Queue<IEvent> q = new();

        q.Enqueue(@event);

        Dispatch(q);
    }

    public void Dispatch(IReadOnlyCollection<IEvent> events)
    {
        _logger.LogDebug("Iniciando disparo de domain events");

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