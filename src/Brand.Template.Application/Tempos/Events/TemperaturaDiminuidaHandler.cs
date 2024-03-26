using Brand.SharedKernel.Abstractions.Events;
using Brand.Template.Domain.Tempos;
using Microsoft.Extensions.Logging;

namespace Brand.Template.Application.Tempos.Events;

internal sealed class TemperaturaDiminuidaHandler(
    ILogger<TemperaturaDiminuidaHandler> logger
) : DomainEventHandler<TemperaturaDiminuida>(logger)
{
    protected override async Task Execute(TemperaturaDiminuida @event, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
    }
}