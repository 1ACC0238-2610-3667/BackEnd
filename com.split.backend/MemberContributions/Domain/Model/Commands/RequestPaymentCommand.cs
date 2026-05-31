namespace com.split.backend.MemberContributions.Domain.Model.Commands;

public record RequestPaymentCommand(string MemberContributionId, decimal Amount);