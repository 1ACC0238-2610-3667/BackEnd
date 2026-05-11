using com.split.backend.Invitations.Domain.Models.Commands;
using com.split.backend.Invitations.Interface.REST.Resources;

namespace com.split.backend.Invitations.Interface.REST.Transform;

public static class CreateInvitationCommandFromResourceAssembler
{
    public static CreateInvitationCommand ToCommandFromResource(CreateInvitationResource resource)
    {
        return new CreateInvitationCommand(
            resource.Email.Trim().ToLowerInvariant(),
            resource.HouseholdId.Trim(),
            resource.Description ?? string.Empty);
    }
}
