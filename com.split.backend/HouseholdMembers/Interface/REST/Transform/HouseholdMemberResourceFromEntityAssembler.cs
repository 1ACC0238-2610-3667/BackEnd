using com.split.backend.HouseholdMembers.Domain.Models.Aggregates;
using com.split.backend.HouseholdMembers.Interface.REST.Resources;

namespace com.split.backend.HouseholdMembers.Interface.REST.Transform;

public static class HouseholdMemberResourceFromEntityAssembler
{
    public static HouseholdMemberResource ToResourceFromEntity(HouseholdMember entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        
        return new HouseholdMemberResource(
            entity.Id,
            entity.HouseholdId,
            entity.UserId,
            entity.IsRepresentative,
            entity.JoinedAt,
            entity.Income,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}

