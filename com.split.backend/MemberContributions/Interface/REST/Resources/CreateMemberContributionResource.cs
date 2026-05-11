namespace com.split.backend.MemberContributions.Interface.REST.Resources;

public record CreateMemberContributionResource(string ContributionId, string MemberId, decimal Amount);