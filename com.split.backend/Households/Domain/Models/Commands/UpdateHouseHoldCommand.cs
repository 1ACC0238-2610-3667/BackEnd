namespace com.split.backend.Households.Domain.Models.Commands;

public record UpdateHouseHoldCommand(
    string Id,
    string Name,
    string Description,
    int MemberCount,
    long RepresentativeId,
    string Currency,
    DateTime? StartDate
);
