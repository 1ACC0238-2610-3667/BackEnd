namespace com.split.backend.Households.Interface.REST.Resources;

public record CreateIncomeAllocationResource(
    long UserId,
    string HouseholdId,
    decimal Percentage);