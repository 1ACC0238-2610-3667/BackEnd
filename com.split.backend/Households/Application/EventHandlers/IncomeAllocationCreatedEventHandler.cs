using com.split.backend.Households.Domain.Models.Events;
using com.split.backend.Shared.Application.Internal.EventHandlers;

namespace com.split.backend.Households.Application.EventHandlers;

public class IncomeAllocationCreatedEventHandler : IEventHandler<IncomeAllocationCreatedEvent>
{
    public Task Handle(IncomeAllocationCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(IncomeAllocationCreatedEvent domainEvent)
    {
        Console.WriteLine("Income Allocation Created: {0}", domainEvent.Id);
        return Task.CompletedTask;
    }
}