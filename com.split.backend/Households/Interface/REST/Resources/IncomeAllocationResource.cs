namespace com.split.backend.Households.Interface.REST.Resources;

public record IncomeAllocationResource(
    string Id, 
    long UserId,
    string HouseholdId,
    decimal Percentage);