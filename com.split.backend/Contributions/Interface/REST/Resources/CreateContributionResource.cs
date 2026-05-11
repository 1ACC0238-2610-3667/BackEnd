namespace com.split.backend.Contributions.Interface.REST.Resources;

public record CreateContributionResource(
    string BillId,
    string HouseholdId,
    string Description,
    string? DeadlineForMembers,
    int? Strategy);
