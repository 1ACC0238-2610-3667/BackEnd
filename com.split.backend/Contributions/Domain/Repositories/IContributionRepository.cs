using com.split.backend.Contributions.Domain.Model.Aggregates;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.Contributions.Domain.Repositories;

public interface IContributionRepository : IBaseRepository<Contribution>
{
    Task<Contribution?> FindByStringIdAsync(string id);
    
    bool ExistsById(string id);
    
    Task<IEnumerable<Contribution?>> FindByBillIdAsync(string billId);
    
    Task<IEnumerable<Contribution?>> FindByHouseholdIdAsync(string userId);
    
}
