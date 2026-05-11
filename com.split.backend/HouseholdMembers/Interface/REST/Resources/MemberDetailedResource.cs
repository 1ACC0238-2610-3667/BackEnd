namespace com.split.backend.HouseholdMembers.Interface.REST.Resources;

public record MemberDetailedResource(
    string HouseholdMemberId,
    int UserId,
    string Name,
    string Email,
    string Role,
    string Status,
    decimal TotalContributed,
    bool IsRepresentative,
    DateTime? JoinedAt);
