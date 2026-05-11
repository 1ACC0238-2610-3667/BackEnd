using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.IAM.Domain.Repositories;

public interface IUserIncomeRepository : IBaseRepository<UserIncome>
{
    Task<UserIncome?> FindByStringIdAsync(string id);
    
    Task<UserIncome?> FindByUserIdAsync(long id);

    bool ExistsByUserId(long userId);

}