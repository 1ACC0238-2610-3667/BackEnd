using com.split.backend.Households.Interface.ACL;

namespace com.split.backend.Households.Application.ACL;

public class HouseHoldContextFacade(
    /*IHouseHoldCommandService houseHoldCommandService,
    IHouseHoldQueryService houseHoldQueryService,*/
    ) : IHouseHoldContextFacade
{
    public Task<string> CreateHouseHold()
    {
        throw new NotImplementedException();
    }

    public Task<string> FetchHouseHoldById(string householdId)
    {
        throw new NotImplementedException();
    }
}