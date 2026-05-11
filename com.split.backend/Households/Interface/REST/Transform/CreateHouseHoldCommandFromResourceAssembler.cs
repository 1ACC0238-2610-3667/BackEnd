using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Interface.REST.Resources;

namespace com.split.backend.Households.Interface.REST.Transform;

public static class CreateHouseHoldCommandFromResourceAssembler
{
    public static CreateHouseholdCommand ToCommandFromResource(CreateHouseHoldResource resource)
    {
        if (resource is null)
        {
            throw new System.ArgumentNullException(nameof(resource));
        }
        
        var name = string.IsNullOrWhiteSpace(resource.Name) ? "New Household" : resource.Name.Trim();
        var description = resource.Description ?? string.Empty;
        var currency = string.IsNullOrWhiteSpace(resource.Currency) ? "USD" : resource.Currency.Trim();
        var memberCount = !resource.MemberCount.HasValue || resource.MemberCount.Value <= 0 ? 1 : resource.MemberCount.Value;
        var representativeId = resource.RepresentativeId;

        if (representativeId <= 0)
            throw new ArgumentException("RepresentativeId is required and must be greater than zero", nameof(resource.RepresentativeId));

        // Accept incoming id but normalize to HH*; otherwise generate new
        string id;
        if (!string.IsNullOrWhiteSpace(resource.Id) && resource.Id.StartsWith("HH"))
            id = resource.Id.Trim();
        else
            id = $"HH{DateTimeOffset.UtcNow.Ticks}";

        var now = DateTime.UtcNow;
        
        return new CreateHouseholdCommand(
            id,
            name,
            representativeId,
            currency,
            description,
            memberCount,
            resource.StartDate ?? now,
            resource.CreatedAt ?? now,
            resource.UpdatedAt ?? now);
    }
    
}
