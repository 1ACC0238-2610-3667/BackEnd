using com.split.backend.Shared.Domain.Model.Events;

namespace com.split.backend.Households.Domain.Models.Events;

public class IncomeAllocationUpdatedEvent(string id) : IEvent
{
    public string Id { get; set; } = id;
}