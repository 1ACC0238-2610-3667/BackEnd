using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Queries;
using com.split.backend.Households.Domain.Repositories;
using com.split.backend.Households.Domain.Services;

namespace com.split.backend.Households.Application.QueryServices;

public class HouseHoldQueryService(
    IHouseHoldRepository houseHoldRepository) : IHouseHoldQueryService
{
    public async Task<HouseHold?> Handle(GetHouseHoldByIdQuery query)
    {
        return await houseHoldRepository.FindByStringIdAsync(query.Id);
    }

    public async Task<IEnumerable<HouseHold?>> Handle(GetAllHouseHoldsQuery query)
    {
        return await houseHoldRepository.ListAsync();
    }

    public async Task<IEnumerable<HouseHold?>> Handle(GetHouseHoldsByRepresentativeId query)
    {
        return await houseHoldRepository.FindByRepresentativeIdAsync(query.RepresentativeId);
    }

    public async Task<IEnumerable<HouseHold>> GetHouseHoldsByRepresentativeId(long representativeId)
    {
        var result = await houseHoldRepository.FindByRepresentativeIdAsync(representativeId);
        return result.Where(h => h != null).Cast<HouseHold>();
    }
}
