namespace RestApi.Common.Abstractions.Domain.Events;

/// <summary>
/// Abstração para disparo de domain events em memória
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Dispara o evento recebido para o(s) seu(s) handler(s)
    /// </summary>
    void Dispatch(IDomainEvent @event);

    /// <summary>
    /// Dispara os events recebidos para o(s) seu(s) handler(s)
    /// </summary>
    void Dispatch(IReadOnlyCollection<IDomainEvent> events);
}