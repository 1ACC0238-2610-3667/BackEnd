using com.split.backend.Invitations.Domain.Models.Aggregates;
using com.split.backend.Invitations.Interface.REST.Resources;

namespace com.split.backend.Invitations.Interface.REST.Transform;

public static class InvitationResourceFromEntityAssembler
{
    public static InvitationResource ToResourceFromEntity(Invitation entity)
    {
        return new InvitationResource(
            entity.Id,
            entity.Email,
            entity.HouseholdId,
            entity.Description,
            entity.Status.ToString(),
            entity.Token,
            entity.ExpiresAt);
    }
}
