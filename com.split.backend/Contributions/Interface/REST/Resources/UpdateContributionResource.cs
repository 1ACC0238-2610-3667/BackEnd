namespace com.split.backend.Contributions.Interface.REST.Resources;

public record UpdateContributionResource(
    string? Description,
    string? DeadlineForMembers,
    int? Strategy);