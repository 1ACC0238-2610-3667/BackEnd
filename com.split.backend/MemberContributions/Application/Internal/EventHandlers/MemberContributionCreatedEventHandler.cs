using com.split.backend.Contributions.Domain.Model.Events;
using com.split.backend.MemberContributions.Domain.Model.Events;
using com.split.backend.Shared.Application.Internal.EventHandlers;

namespace com.split.backend.MemberContributions.Application.Internal.EventHandlers;

public class MemberContributionCreatedEventHandler : IEventHandler<MemberContributionCreatedEvent>
{
    public Task Handle(MemberContributionCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(MemberContributionCreatedEvent domainEvent)
    {
        Console.WriteLine("MemberContribution Created: {0}", domainEvent.Id);
        return Task.CompletedTask;
    } 
    
}