using System.Diagnostics.Tracing;
using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Interface.REST.Resources;

namespace com.split.backend.Households.Interface.REST.Transform;

public static class IncomeAllocationResourceFromEntityAssembler
{
    public static IncomeAllocationResource ToResourceFromEntity(IncomeAllocation entity)
    {
        return new IncomeAllocationResource(
            entity.Id,
            entity.UserId,
            entity.HouseholdId,
            entity.Percentage);
    }
}