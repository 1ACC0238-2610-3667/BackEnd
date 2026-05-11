namespace com.split.backend.HouseholdMembers.Interface.ACL;

public interface IUserContextFacade
{
    Task<bool> ExistsUserByIdAsync(int userId);
    
    Task<bool> IsValidUserAsync(int userId);
}

