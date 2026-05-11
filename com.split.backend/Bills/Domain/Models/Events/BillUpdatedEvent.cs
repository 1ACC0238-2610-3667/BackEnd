using com.split.backend.Shared.Domain.Model.Events;

namespace com.split.backend.Bills.Domain.Models.Events;

public class BillUpdatedEvent(string description) : IEvent
{
    public string Description { get; } = description;
    
}