namespace com.split.backend.HouseholdMembers.Domain.Models.Commands;

public record CreateHouseholdMemberCommand(
    string HouseholdId,
    int UserId,
    bool IsRepresentative,
    decimal Income
);

