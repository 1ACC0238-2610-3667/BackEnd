using com.split.backend.HouseholdMembers.Interface.ACL;
using com.split.backend.Households.Domain.Repositories;

namespace com.split.backend.HouseholdMembers.Application.ACL;

public class HouseholdContextFacade(IHouseHoldRepository householdRepository) 
    : IHouseholdContextFacade
{
    public async Task<bool> ExistsHouseholdByIdAsync(string householdId)
    {
        var household = await householdRepository.FindByStringIdAsync(householdId);
        return household != null;
    }

    public async Task<bool> IsValidHouseholdAsync(string householdId)
    {
        return await ExistsHouseholdByIdAsync(householdId);
    }
}

