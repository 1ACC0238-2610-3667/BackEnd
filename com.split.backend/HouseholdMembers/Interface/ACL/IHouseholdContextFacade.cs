namespace com.split.backend.HouseholdMembers.Interface.ACL;

public interface IHouseholdContextFacade
{
    Task<bool> ExistsHouseholdByIdAsync(string householdId);
    
    Task<bool> IsValidHouseholdAsync(string householdId);
}

