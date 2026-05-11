namespace com.split.backend.MemberContributions.Domain.Model.Commands;

public record CreateMemberContributionCommand(string ContributionId, string MemberId, decimal Amount);