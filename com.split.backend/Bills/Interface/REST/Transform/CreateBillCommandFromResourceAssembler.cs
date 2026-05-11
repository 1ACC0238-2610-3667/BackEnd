using com.split.backend.Bills.Domain.Models.Commands;
using com.split.backend.Bills.Interface.REST.Resources;

namespace com.split.backend.Bills.Interface.REST.Transform;

public static class CreateBillCommandFromResourceAssembler
{
    public static CreateBillCommand ToCommandFromResource(CreateBillResource resource)
    {
        return new CreateBillCommand(
            resource.HouseHoldId,
            resource.Description,
            resource.Amount,
            resource.CreatedBy,
            resource.PaymentDate);
    }
}
