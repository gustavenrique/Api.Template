using MediatR;

namespace SharedKernel.Abstractions.Events;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IEvent;