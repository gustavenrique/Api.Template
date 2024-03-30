using Brand.Template.Domain.Tempos;
using Microsoft.Extensions.Logging;
using Brand.Common.Abstractions.Domain.Events;

namespace Brand.Template.Application.Tempos.Events;

internal sealed class TemperaturaDiminuidaHandler(
    ILogger<TemperaturaDiminuidaHandler> logger
) : DomainEventHandler<TemperaturaDiminuida>(logger)
{
    readonly ILogger<TemperaturaDiminuidaHandler> _logger = logger;

    protected override Task Execute(TemperaturaDiminuida @event, CancellationToken cancellationToken)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("[Handle] Evento recebido: {@Evento}", @event);
        }

        return Task.CompletedTask;
    }
}