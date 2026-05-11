namespace com.split.backend.HouseholdMembers.Interface.REST.Resources;

public record HouseholdMemberResource(
    string Id,
    string HouseholdId,
    int UserId,
    bool IsRepresentative,
    DateTime JoinedAt,
    decimal Income,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
