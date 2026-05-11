namespace com.split.backend.Bills.Application.Internal.QueryServices;
using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Bills.Domain.Models.Queries;
using com.split.backend.Bills.Domain.Repositories;
using com.split.backend.Bills.Domain.Services;

public class BillQueryService(IBillRepository billRepository) : IBillQueryService
{
    public async Task<IEnumerable<Bill?>> Handle(GetBillsByHouseholdIdQuery query)
    {
        return await billRepository.FindByHouseholdIdAsync(query.HouseholdId);
    }

    public async Task<IEnumerable<Bill?>> Handle(GetAllBillsQuery query)
    {
        return await billRepository.ListAsync();
    }
}