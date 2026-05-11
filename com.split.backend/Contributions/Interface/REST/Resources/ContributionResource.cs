namespace com.split.backend.Contributions.Interface.REST.Resources;

public record ContributionResource(
    string Id,
    string BillId,
    string HouseholdId,
    string Description,
    string DeadlineForMembers,
    int Strategy);