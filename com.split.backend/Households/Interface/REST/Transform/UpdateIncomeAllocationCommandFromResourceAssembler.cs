using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Interface.REST.Resources;

namespace com.split.backend.Households.Interface.REST.Transform;

public static class UpdateIncomeAllocationCommandFromResourceAssembler
{
    public static UpdateIncomeAllocationCommand ToCommandFromResource(string id, UpdateIncomeAllocationResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource));
        
        return new UpdateIncomeAllocationCommand(
            id,
            resource.UserId,
            resource.HouseholdId,
            resource.Percentage);
    }
}