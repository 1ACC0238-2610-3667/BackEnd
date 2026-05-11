namespace com.split.backend.Households.Interface.REST.Resources;

public record CreateHouseHoldResource(
    string? Id,
    string Name,
    long RepresentativeId,
    string Currency,
    string? Description,
    int? MemberCount,
    DateTime? StartDate,
    DateTime? CreatedAt,
    DateTime? UpdatedAt);
