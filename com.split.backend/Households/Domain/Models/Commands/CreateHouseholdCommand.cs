namespace com.split.backend.Households.Domain.Models.Commands;

public record CreateHouseholdCommand(
    string? Id,
    string Name,
    long RepresentativeId,
    string Currency,
    string? Description,
    int MemberCount,
    DateTime? StartDate,
    DateTime? CreatedAt,
    DateTime? UpdatedAt
);
