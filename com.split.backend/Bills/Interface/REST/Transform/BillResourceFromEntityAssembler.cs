using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Bills.Interface.REST.Resources;

namespace com.split.backend.Bills.Interface.REST.Transform;

public static class BillResourceFromEntityAssembler
{
    public static BillResource ToResourceFromEntity(Bill entity)
    {
        return new BillResource(entity.Id, entity.HouseholdId,
            entity.Description, entity.Amount, entity.CreatedBy, 
            entity.PaymentDate?.ToString("O") ?? string.Empty,
            entity.CreatedDate?.ToString("O") ?? string.Empty,
            entity.UpdatedDate?.ToString("O") ?? string.Empty);
    }
    
}
