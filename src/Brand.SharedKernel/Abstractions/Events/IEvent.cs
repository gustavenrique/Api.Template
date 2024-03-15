using MediatR;

namespace SharedKernel.Abstractions.Events;

/// <summary>
/// Marker interface para representação de um domain event
/// </summary>
public interface IEvent : INotification;