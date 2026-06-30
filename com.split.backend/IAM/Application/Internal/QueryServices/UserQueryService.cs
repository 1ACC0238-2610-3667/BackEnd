using System.Linq;
using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.Queries;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.IAM.Domain.Services;

namespace com.split.backend.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUsersByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        var users = await userRepository.ListAsync();
        if (!string.IsNullOrWhiteSpace(query.HouseholdId))
        {
            users = users.Where(u => u.HouseholdId == query.HouseholdId);
        }
        if (!string.IsNullOrWhiteSpace(query.Role))
        {
            users = users.Where(u => u.Role.ToString().Equals(query.Role, System.StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrWhiteSpace(query.Email))
        {
            users = users.Where(u => u.Email != null && u.Email.Address.Equals(query.Email, System.StringComparison.OrdinalIgnoreCase));
        }
        return users;
    }

    public async Task<User?> Handle(GetUserByMainHouseHoldId query)
    {
        return await userRepository.FindByHouseHoldIdAsync(query.HouseHoldId);
    }
}
