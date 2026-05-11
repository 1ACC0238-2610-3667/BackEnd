using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.ValueObjects;
using com.split.backend.IAM.Interface.REST.Resources;

namespace com.split.backend.IAM.Interface.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token, bool wasNewUser)
    {
        return new AuthenticatedUserResource(
            user.Id,
            user.Email.ToString(),
            token,
            wasNewUser,
            user.HouseholdId ?? string.Empty,
            user.Role.ToString(),
            user.Plan?.ToString() ?? EPlan.Free.ToString());
    }
}
