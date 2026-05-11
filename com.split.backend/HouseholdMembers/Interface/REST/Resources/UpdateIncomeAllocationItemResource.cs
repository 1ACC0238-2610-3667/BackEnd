namespace com.split.backend.HouseholdMembers.Interface.REST.Resources;

public record UpdateIncomeAllocationItemResource(
    string? HouseholdId,
    decimal Percentage,
    long? UserId);
