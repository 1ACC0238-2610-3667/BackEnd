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
        return await userRepository.ListAsync();
    }

    public async Task<User?> Handle(GetUserByMainHouseHoldId query)
    {
        return await userRepository.FindByHouseHoldIdAsync(query.HouseHoldId);
    }
}
