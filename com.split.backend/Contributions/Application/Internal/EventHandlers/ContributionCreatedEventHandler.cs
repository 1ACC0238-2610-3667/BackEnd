using com.split.backend.Contributions.Domain.Model.Events;
using com.split.backend.Shared.Application.Internal.EventHandlers;

namespace com.split.backend.Contributions.Application.Internal.EventHandlers;

public class ContributionCreatedEventHandler : IEventHandler<ContributionCreatedEvent>
{
    public Task Handle(ContributionCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(ContributionCreatedEvent domainEvent)
    {
        Console.WriteLine("Bill Created: {0}", domainEvent.Id);
        return Task.CompletedTask;
    }
}