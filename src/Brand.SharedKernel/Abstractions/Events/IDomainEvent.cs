using MediatR;

namespace Brand.SharedKernel.Abstractions.Events;

/// <summary>
/// Marker interface para representação de um domain event
/// </summary>
public interface IDomainEvent : INotification;