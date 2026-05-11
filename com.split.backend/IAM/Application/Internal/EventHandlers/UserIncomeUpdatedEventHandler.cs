using com.split.backend.IAM.Domain.Model.Events;
using com.split.backend.Shared.Application.Internal.EventHandlers;

namespace com.split.backend.IAM.Application.Internal.EventHandlers;

public class UserIncomeUpdatedEventHandler : IEventHandler<UserIncomeUpdatedEvent>
{
    public Task Handle(UserIncomeUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(UserIncomeUpdatedEvent domainEvent)
    {
        Console.WriteLine("UserIncome Instance updated: {0}", domainEvent.Id);
        return Task.CompletedTask;
    } 
}