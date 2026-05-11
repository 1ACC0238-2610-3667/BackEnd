using com.split.backend.Bills.Interface.ACL;

namespace com.split.backend.Bills.Application.ACL;

public class BillsContextFacade() : IBillsContextFacade
{
    public Task<string> CreateBill()
    {
        throw new NotImplementedException();
    }

    public Task<string> FetchBillById(string billId)
    {
        throw new NotImplementedException();
    }
}