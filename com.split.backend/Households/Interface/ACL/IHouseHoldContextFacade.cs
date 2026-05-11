namespace com.split.backend.Households.Interface.ACL;

public interface IHouseHoldContextFacade
{
    Task<string> CreateHouseHold();
    
    Task<string> FetchHouseHoldById(string householdId);
}