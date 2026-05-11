using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Interface.REST.Resources;

namespace com.split.backend.IAM.Interface.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(
            Id: entity.Id,

            Email: entity.Email?.Address ?? string.Empty,

            PersonName: entity.PersonName?.FirstName 
                        ?? string.Empty,

            HouseHoldId: entity.HouseholdId ?? string.Empty,

            Role: entity.Role.ToString() 
                  ?? "Unknown",

            Plan: entity.Plan?.ToString() 
                  ?? "Unspecified",

            Photo: entity.Photo?.AbsoluteUri 
                   ?? string.Empty,

            ProfileLockedUntil: entity.ProfileLockedUntil?.ToString("O") 
                                ?? string.Empty,

            IsNewUser: entity.IsNewUser?.ToString() 
                       ?? "false"
        );
    }
}