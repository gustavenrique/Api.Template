using Brand.Template.Domain.Tempos;
using Microsoft.Extensions.Logging;
using SharedKernel.Abstractions.Events;

namespace Brand.Template.Application.Tempos;
internal sealed class TemperaturaDiminuidaHandler(
    ILogger<TemperaturaDiminuidaHandler> logger
) : IEventHandler<TemperaturaDiminuida>
{
    readonly ILogger<TemperaturaDiminuidaHandler> _logger = logger;

    public async Task Handle(TemperaturaDiminuida @event, CancellationToken cancellationToken)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Evento recebido: {@Evento}", @event);
        }

        await Task.Delay(5000, cancellationToken);

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Evento processado");
        }
    }
}