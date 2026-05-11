using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Interface.REST.Resources;

namespace com.split.backend.Households.Interface.REST.Transform;

public static class CreateIncomeAllocationCommandFromResourceAssembler
{
    public static CreateIncomeAllocationCommand ToCommandFromResource(CreateIncomeAllocationResource resource)
    {
        if(resource is null)
            throw new ArgumentNullException(nameof(resource));

        return new CreateIncomeAllocationCommand(
            resource.UserId,
            resource.HouseholdId,
            resource.Percentage);
    }
    
}