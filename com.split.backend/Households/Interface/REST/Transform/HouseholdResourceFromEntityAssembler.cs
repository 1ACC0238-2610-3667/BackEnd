using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Domain.Models.ValueObjects;
using com.split.backend.Households.Interface.REST.Resources;

namespace com.split.backend.Households.Interface.REST.Transform;

public static class HouseholdResourceFromEntityAssembler
{
    public static HouseHoldResource ToResourceFromEntity(HouseHold entity)
    {
        return new HouseHoldResource(
            entity.Id,
            entity.Name,
            entity.Description ?? string.Empty,
            entity.MemberCount,
            entity.RepresentativeId,
            entity.Currency.ToString(),
            entity.StartDate?.ToString("O") ?? string.Empty,
            entity.CreatedDate?.ToString("O") ?? string.Empty,
            entity.UpdatedDate?.ToString("O") ?? string.Empty);
    }
}
