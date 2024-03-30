using Microsoft.Extensions.Logging;
using RestApi.Common.Abstractions.Domain.Events;
using RestApi.Template.Domain.Tempos.Event;

namespace RestApi.Template.Application.Tempos.Events;

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