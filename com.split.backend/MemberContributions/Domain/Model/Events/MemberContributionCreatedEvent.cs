using com.split.backend.Shared.Domain.Model.Events;

namespace com.split.backend.MemberContributions.Domain.Model.Events;

public class MemberContributionCreatedEvent(string id) : IEvent
{
    public string Id { get; } = id;
}