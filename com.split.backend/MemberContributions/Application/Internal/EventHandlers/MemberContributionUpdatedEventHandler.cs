using com.split.backend.MemberContributions.Domain.Model.Events;
using com.split.backend.Shared.Application.Internal.EventHandlers;

namespace com.split.backend.MemberContributions.Application.Internal.EventHandlers;

public class MemberContributionUpdatedEventHandler : IEventHandler<MemberContributionUpdatedEvent>
{
    public Task Handle(MemberContributionUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(MemberContributionUpdatedEvent domainEvent)
    {
        Console.WriteLine("MemberContribution Updated: {0}", domainEvent.Id);
        return Task.CompletedTask;
    } 
}