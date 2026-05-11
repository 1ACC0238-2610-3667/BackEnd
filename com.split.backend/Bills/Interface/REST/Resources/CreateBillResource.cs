namespace com.split.backend.Bills.Interface.REST.Resources;

public record CreateBillResource(
   string HouseHoldId,
   string Description,
   decimal Amount,
   long CreatedBy,
   string PaymentDate
);