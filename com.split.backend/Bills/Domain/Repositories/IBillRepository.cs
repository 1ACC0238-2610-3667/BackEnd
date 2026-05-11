using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.Bills.Domain.Repositories;

public interface IBillRepository : IBaseRepository<Bill>
{
    Task<Bill?> FindByStringIdAsync(string id);
    
    bool ExistsById(string id);
    
    Task<IEnumerable<Bill?>> FindByHouseholdIdAsync(string householdId);
    
}