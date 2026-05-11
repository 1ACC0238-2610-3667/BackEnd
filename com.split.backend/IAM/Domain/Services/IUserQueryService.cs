using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.Queries;

namespace com.split.backend.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<User?> Handle(GetUsersByIdQuery query);
    
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);

    Task<User?> Handle(GetUserByMainHouseHoldId query);
}
