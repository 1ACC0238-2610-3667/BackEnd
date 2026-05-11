namespace com.split.backend.Contributions.Domain.Model.Commands;

public record CreateContributionCommand(
    string BillId,
    string HouseholdId,
    string Description,
    string? DeadlineForMembers,
    int? Strategy);
