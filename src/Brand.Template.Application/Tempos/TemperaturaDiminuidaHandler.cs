using Brand.Template.Domain.Tempos;
using Microsoft.Extensions.Logging;
using SharedKernel.Abstractions.Events;

namespace Brand.Template.Application.Tempos;
internal sealed class TemperaturaDiminuidaHandler(
    ILogger<TemperaturaDiminuidaHandler> logger
) : IEventHandler<TemperaturaDiminuida>
{
    readonly ILogger<TemperaturaDiminuidaHandler> _logger = logger;

    public Task Handle(TemperaturaDiminuida @event, CancellationToken cancellationToken)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("[Handle] Evento recebido: {@Evento}", @event);
        }

        return Task.CompletedTask;
    }
}