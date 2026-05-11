using com.split.backend.Shared.Domain.Model.Events;

namespace com.split.backend.Contributions.Domain.Model.Events;

public class ContributionUpdatedEvent(string id) : IEvent
{
    public string Id { get; } = id;

}