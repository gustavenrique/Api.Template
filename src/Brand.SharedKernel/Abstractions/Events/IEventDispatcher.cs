namespace SharedKernel.Abstractions.Events;

/// <summary>
/// Abstração para disparo de domain events em memória
/// </summary>
public interface IEventDispatcher
{
    /// <summary>
    /// Dispara o evento recebido para o(s) seu(s) handler(s)
    /// </summary>
    void Dispatch(IEvent @event);

    /// <summary>
    /// Dispara os events recebidos para o(s) seu(s) handler(s)
    /// </summary>
    void Dispatch(IReadOnlyCollection<IEvent> events);
}