using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Interface.REST.Resources;

namespace com.split.backend.Households.Interface.REST.Transform;

public static class UpdateHouseHoldCommandFromResourceAssembler
{
    public static UpdateHouseHoldCommand ToCommandFromResource(string id, UpdateHouseHoldResource resource)
    {
        var name = string.IsNullOrWhiteSpace(resource.Name) ? string.Empty : resource.Name.Trim();
        var description = resource.Description ?? string.Empty;
        var currency = string.IsNullOrWhiteSpace(resource.Currency) ? "USD" : resource.Currency.Trim();
        var memberCount = resource.MemberCount <= 0 ? 1 : resource.MemberCount;
        return new UpdateHouseHoldCommand(
            id,
            name,
            description,
            memberCount,
            0, // RepresentativeId stays unchanged unless provided via another command; keep existing value
            currency,
            resource.StartDate
        );
    }
}
