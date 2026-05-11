namespace com.split.backend.MemberContributions.Interface.REST.Resources;

public record MemberContributionResource(string Id, string ContributionId, string MemberId,
    decimal Amount, string Status, string PayedAt);