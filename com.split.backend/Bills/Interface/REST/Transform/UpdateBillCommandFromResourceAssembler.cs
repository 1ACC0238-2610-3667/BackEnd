using com.split.backend.Bills.Domain.Models.Commands;
using com.split.backend.Bills.Interface.REST.Resources;

namespace com.split.backend.Bills.Interface.REST.Transform;

public static class UpdateBillCommandFromResourceAssembler
{
    public static UpdateBillCommand ToCommandFromResource(string id, UpdateBillResource resource)
    {
        if(resource is null) throw new ArgumentNullException(nameof(resource));

        return new UpdateBillCommand(
            id,
            resource.Description,
            resource.Amount,
            resource.PaymentDate);
    }
}