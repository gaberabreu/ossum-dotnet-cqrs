using MediatR;

namespace Ossum.CQRS;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : DomainEventBase;
