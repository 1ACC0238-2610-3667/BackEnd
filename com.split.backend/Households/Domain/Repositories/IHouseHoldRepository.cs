using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.Households.Domain.Repositories;

public interface IHouseHoldRepository : IBaseRepository<HouseHold>
{
    Task<HouseHold?> FindByStringIdAsync(string id);
    
    bool ExistsById(string representativeId);
    
    Task<IEnumerable<HouseHold?>> FindByRepresentativeIdAsync(long representativeId);
}
