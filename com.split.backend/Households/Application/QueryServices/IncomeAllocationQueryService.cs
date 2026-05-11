using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Queries;
using com.split.backend.Households.Domain.Repositories;
using com.split.backend.Households.Domain.Services;

namespace com.split.backend.Households.Application.QueryServices;

public class IncomeAllocationQueryService(
    IIncomeAllocationRepository incomeAllocationRepository): IIncomeAllocationQueryService
{
    public async Task<IEnumerable<IncomeAllocation?>> Handle(GetAllIncomeAllocationQuery query)
    {
        return await incomeAllocationRepository.ListAsync();
    }

    public async Task<IEnumerable<IncomeAllocation?>> Handle(GetIncomeAllocationByUserIdQuery query)
    {
        return await incomeAllocationRepository.FindByUserIdAsync(query.UserId);
    }

    public async Task<IEnumerable<IncomeAllocation?>> Handle(GetIncomeAllocationByHouseHoldIdQuery query)
    {
        return await incomeAllocationRepository.FindByHouseholdIdAsync(query.HouseholdId);
    }
}