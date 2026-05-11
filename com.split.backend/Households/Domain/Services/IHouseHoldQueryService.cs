using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Queries;

namespace com.split.backend.Households.Domain.Services;

public interface IHouseHoldQueryService
{
    Task<IEnumerable<HouseHold?>> Handle(GetAllHouseHoldsQuery query);

    Task<IEnumerable<HouseHold?>> Handle(GetHouseHoldsByRepresentativeId query);
    
    Task<HouseHold?> Handle(GetHouseHoldByIdQuery query);

    Task<IEnumerable<HouseHold>> GetHouseHoldsByRepresentativeId(long representativeId);
}
