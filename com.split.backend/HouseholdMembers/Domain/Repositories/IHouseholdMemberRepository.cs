using com.split.backend.HouseholdMembers.Domain.Models.Aggregates;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.HouseholdMembers.Domain.Repositories;

public interface IHouseholdMemberRepository : IBaseRepository<HouseholdMember>
{
    Task<HouseholdMember?> FindByIdAsync(string id);
    
    Task<HouseholdMember?> FindByHouseholdIdAndUserIdAsync(string householdId, int userId);
    Task<bool> ExistsByHouseholdIdAndUserIdAsync(string householdId, int userId);
    
    Task<IEnumerable<HouseholdMember>> FindByHouseholdIdAsync(string householdId);
    
    Task<IEnumerable<HouseholdMember>> FindByUserIdAsync(int userId);
    
    Task<HouseholdMember?> FindRepresentativeByHouseholdIdAsync(string householdId);
    
    bool ExistsByHouseholdIdAndUserId(string householdId, int userId);
    
    Task<int> CountByHouseholdIdAsync(string householdId);
}
