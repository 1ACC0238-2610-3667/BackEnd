using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.Households.Domain.Repositories;

public interface IIncomeAllocationRepository : IBaseRepository<IncomeAllocation>
{
    Task<IncomeAllocation?> FindByStringIdAsync(string id);
    
    bool ExistsById(string id);
    
    Task<IEnumerable<IncomeAllocation?>> FindByHouseholdIdAsync(string householdId);
    
    Task<IEnumerable<IncomeAllocation>> FindByUserIdAsync(long userId);
    
}