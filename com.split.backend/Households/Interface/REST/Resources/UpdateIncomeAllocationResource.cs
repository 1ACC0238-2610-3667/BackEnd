namespace com.split.backend.Households.Interface.REST.Resources;

public record UpdateIncomeAllocationResource(
    string Id,
    long? UserId,
    string? HouseholdId,
    decimal? Percentage);