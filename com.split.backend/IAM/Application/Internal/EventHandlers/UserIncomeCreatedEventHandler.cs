using com.split.backend.IAM.Domain.Model.Events;
using com.split.backend.Shared.Application.Internal.EventHandlers;
using Humanizer;

namespace com.split.backend.IAM.Application.Internal.EventHandlers;

public class UserIncomeCreatedEventHandler : IEventHandler<UserIncomeCreatedEvent> 
{
    public Task Handle(UserIncomeCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(UserIncomeCreatedEvent domainEvent)
    {
        Console.WriteLine("UserIncome Instance created: {0}", domainEvent.Id);
        return Task.CompletedTask;
    }
}