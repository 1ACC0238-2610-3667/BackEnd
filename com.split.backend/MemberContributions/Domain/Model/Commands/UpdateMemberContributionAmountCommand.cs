namespace com.split.backend.MemberContributions.Domain.Model.Commands;

public record UpdateMemberContributionAmountCommand(string Id, decimal Amount);