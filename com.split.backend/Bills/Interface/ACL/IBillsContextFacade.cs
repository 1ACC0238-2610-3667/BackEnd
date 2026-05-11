namespace com.split.backend.Bills.Interface.ACL;

public interface IBillsContextFacade
{
    public interface IBillsContextFacade
    {
        Task<string> CreateBill();                 
        Task<string> FetchBillById(string billId); 
    }
}