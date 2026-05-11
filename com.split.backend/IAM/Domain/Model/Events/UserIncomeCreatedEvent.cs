using com.split.backend.Shared.Domain.Model.Events;

namespace com.split.backend.IAM.Domain.Model.Events;

public class UserIncomeCreatedEvent(string id) : IEvent
{
    public string Id { get; } = id;
}