using System.Linq;
using com.split.backend.HouseholdMembers.Domain.Models.Commands;
using com.split.backend.HouseholdMembers.Interface.REST.Resources;

namespace com.split.backend.HouseholdMembers.Interface.REST.Transform;

public static class UpdateHouseholdMemberCommandFromResourceAssembler
{
    public static UpdateHouseholdMemberCommand ToCommandFromResource(string id, UpdateHouseholdMemberResource resource)
    {
        if (resource is null)
        {
            throw new ArgumentNullException(nameof(resource));
        }
        
        return new UpdateHouseholdMemberCommand(
            id,
            resource.HouseholdId,
            resource.UserId,
            resource.IsRepresentative,
            resource.Income,
            resource.Allocations?.Select(a => new IncomeAllocationItem(
                a.HouseholdId,
                a.Percentage,
                a.UserId)) ?? Enumerable.Empty<IncomeAllocationItem>());
    }
}
