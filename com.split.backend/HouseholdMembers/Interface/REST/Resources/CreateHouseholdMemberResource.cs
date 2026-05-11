namespace com.split.backend.HouseholdMembers.Interface.REST.Resources;

// Legacy resource kept for direct creation by userId (not used for invitation flow)
public record CreateHouseholdMemberResource(
    string HouseholdId, 
    int UserId,
    bool IsRepresentative,
    decimal Income
);

