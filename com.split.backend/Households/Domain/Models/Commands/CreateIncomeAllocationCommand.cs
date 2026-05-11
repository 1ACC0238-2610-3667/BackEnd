namespace com.split.backend.Households.Domain.Models.Commands;

public record CreateIncomeAllocationCommand(
    long UserId,
    string HouseholdId,
    decimal Percentage);