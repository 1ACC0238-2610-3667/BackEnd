namespace com.split.backend.HouseholdMembers.Domain.Models.Commands;

public record UpdateHouseholdMemberCommand(
    string Id,
    string? HouseholdId,
    int? UserId,
    bool? IsRepresentative,
    decimal? Income,
    IEnumerable<IncomeAllocationItem> Allocations
);
