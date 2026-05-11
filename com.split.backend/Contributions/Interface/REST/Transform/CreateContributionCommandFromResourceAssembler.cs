using com.split.backend.Contributions.Domain.Model.Commands;
using com.split.backend.Contributions.Interface.REST.Resources;

namespace com.split.backend.Contributions.Interface.REST.Transform;

public static class CreateContributionCommandFromResourceAssembler
{
    public static CreateContributionCommand ToCommandFromResource(CreateContributionResource resource)
    {
        if(resource is null) throw new ArgumentNullException(nameof(resource));

        return new CreateContributionCommand(
            resource.BillId,
            resource.HouseholdId,
            resource.Description ?? string.Empty,
            resource.DeadlineForMembers,
            resource.Strategy);
    }
    
}
