using com.split.backend.HouseholdMembers.Interface.ACL;
using com.split.backend.IAM.Domain.Repositories;

namespace com.split.backend.HouseholdMembers.Application.ACL;

public class UserContextFacade(IUserRepository userRepository) : IUserContextFacade
{
    public async Task<bool> ExistsUserByIdAsync(int userId)
    {
        var user = await userRepository.FindByIdAsync(userId);
        return user != null;
    }

    public async Task<bool> IsValidUserAsync(int userId)
    {
        return await ExistsUserByIdAsync(userId);
    }
}

