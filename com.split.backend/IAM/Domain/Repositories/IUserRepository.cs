using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(string email);
    
    bool ExistsByEmail(string email);
    
    Task<User?> FindByHouseHoldIdAsync(string houseHoldId);
}