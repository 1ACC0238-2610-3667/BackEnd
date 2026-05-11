using com.split.backend.Bills.Domain.Models.Events;
using com.split.backend.Shared.Application.Internal.EventHandlers;

namespace com.split.backend.Bills.Application.Internal.EventHandlers;

public class BillCreateEventHandler : IEventHandler<BillCreatedEvent>
{
    public Task Handle(BillCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(BillCreatedEvent domainEvent)
    {
        Console.WriteLine("Bill Created: {0}", domainEvent.Description);
        return Task.CompletedTask;
    }
    
}