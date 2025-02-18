using MediatR;

namespace Ossum.CQRS;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}