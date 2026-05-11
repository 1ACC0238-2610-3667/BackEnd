namespace com.split.backend.Bills.Domain.Models.Commands;

public record CreateBillCommand(
    string HouseholdId,
    string Description,
    decimal Amount,
    long CreatedBy,
    string PaymentDate
);