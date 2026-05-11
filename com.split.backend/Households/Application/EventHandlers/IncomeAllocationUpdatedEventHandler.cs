using com.split.backend.Households.Domain.Models.Events;
using com.split.backend.Shared.Application.Internal.EventHandlers;

namespace com.split.backend.Households.Application.EventHandlers;

public class IncomeAllocationUpdatedEventHandler : IEventHandler<IncomeAllocationUpdatedEvent>
{
    public Task Handle(IncomeAllocationUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(IncomeAllocationUpdatedEvent domainEvent)
    {
        Console.WriteLine("Income Allocation Updated: {0}", domainEvent.Id);
        return Task.CompletedTask;
    }
    
}