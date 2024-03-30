using MediatR;

namespace RestApi.Common.Abstractions.Domain.Events;

/// <summary>
/// Marker interface para representação de um domain event
/// </summary>
public interface IDomainEvent : INotification;