using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Queries;

namespace com.split.backend.Households.Domain.Services;

public interface IIncomeAllocationQueryService
{
    public Task<IEnumerable<IncomeAllocation?>> Handle(GetAllIncomeAllocationQuery query);

    public Task<IEnumerable<IncomeAllocation?>> Handle(GetIncomeAllocationByUserIdQuery query);
    
    public Task<IEnumerable<IncomeAllocation?>> Handle(GetIncomeAllocationByHouseHoldIdQuery query);
}