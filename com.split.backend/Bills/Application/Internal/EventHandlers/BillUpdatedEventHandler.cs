using com.split.backend.Bills.Domain.Models.Events;
using com.split.backend.Shared.Application.Internal.EventHandlers;


namespace com.split.backend.Bills.Application.Internal.EventHandlers;

public class BillUpdatedEventHandler : IEventHandler<BillUpdatedEvent>
{
    public Task Handle(BillUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(BillUpdatedEvent domainEvent)
    {
        Console.WriteLine("Bill Updated: {0}", domainEvent.Description);
        return Task.CompletedTask;
    }
    
}