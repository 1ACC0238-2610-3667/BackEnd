using com.split.backend.Shared.Domain.Model.Events;

namespace com.split.backend.Households.Domain.Models.Events;

public class IncomeAllocationCreatedEvent(string id) : IEvent
{
    public string Id { get; set; } = id;

}