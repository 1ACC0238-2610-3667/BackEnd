namespace com.split.backend.Households.Domain.Models.Commands;

public record IncomeAllocationUpsertCommand(
    long UserId,
    string HouseholdId,
    decimal Percentage);
