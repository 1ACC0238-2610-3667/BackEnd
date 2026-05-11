using com.split.backend.Contributions.Domain.Model.Events;

namespace com.split.backend.Contributions.Application.Internal.EventHandlers;

public class ContributionUpdatedEventHandler
{
    public Task Handle(ContributionUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(ContributionUpdatedEvent domainEvent)
    {
        Console.WriteLine("Contribution Updated: {0}", domainEvent.Id);
        return Task.CompletedTask;
    }

}