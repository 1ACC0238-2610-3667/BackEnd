using com.split.backend.HouseholdMembers.Domain.Models.Commands;
using com.split.backend.HouseholdMembers.Interface.REST.Resources;

namespace com.split.backend.HouseholdMembers.Interface.REST.Transform;

public static class CreateHouseholdMemberCommandFromResourceAssembler
{
    public static CreateHouseholdMemberCommand ToCommandFromResource(CreateHouseholdMemberResource resource)
    {
        if (resource is null) throw new ArgumentNullException(nameof(resource));

        return new CreateHouseholdMemberCommand(
            resource.HouseholdId,
            resource.UserId,
            resource.IsRepresentative,
            resource.Income
        );
    }
}
