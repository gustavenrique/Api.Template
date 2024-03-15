using Brand.Template.Domain.Tempos;
using Microsoft.Extensions.Logging;
using SharedKernel.Abstractions.Events;

namespace Brand.Template.Application.Tempos;
internal sealed class TemperaturaDiminuidaEventHandler(
    ILogger<TemperaturaDiminuidaEventHandler> logger
) : IEventHandler<TemperaturaDiminuidaEvent>
{
    readonly ILogger<TemperaturaDiminuidaEventHandler> _logger = logger;

    public Task Handle(TemperaturaDiminuidaEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Handle] Evento recebido: {@Evento}", @event);

        return Task.CompletedTask;
    }
}
