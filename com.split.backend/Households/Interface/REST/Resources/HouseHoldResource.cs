namespace com.split.backend.Households.Interface.REST.Resources;

public record HouseHoldResource(
    string Id,
    string Name,
    string Description,
    int MemberCount,
    long RepresentativeId,
    string Currency,
    string StartDate,
    string CreatedAt,
    string UpdatedAt);
