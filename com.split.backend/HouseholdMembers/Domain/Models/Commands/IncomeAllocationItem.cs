namespace com.split.backend.HouseholdMembers.Domain.Models.Commands;

public record IncomeAllocationItem(
    string? HouseholdId,
    decimal Percentage,
    long? UserId);
