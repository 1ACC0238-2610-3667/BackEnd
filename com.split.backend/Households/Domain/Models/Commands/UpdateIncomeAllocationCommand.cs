namespace com.split.backend.Households.Domain.Models.Commands;

public record UpdateIncomeAllocationCommand(string Id,
    long? UserId,
    string? HouseholdId,
    decimal? Percentage);