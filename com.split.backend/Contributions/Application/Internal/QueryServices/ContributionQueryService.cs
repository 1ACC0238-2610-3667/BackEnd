using com.split.backend.Contributions.Domain.Model.Aggregates;
using com.split.backend.Contributions.Domain.Model.Queries;
using com.split.backend.Contributions.Domain.Repositories;
using com.split.backend.Contributions.Domain.Services;

namespace com.split.backend.Contributions.Application.Internal.QueryServices;

public class ContributionQueryService(IContributionRepository contributionRepository) : IContributionQueryService
{
    public async Task<IEnumerable<Contribution?>> Handle(GetAllContributionsQuery query)
    {
        return await contributionRepository.ListAsync();
    }

    public async Task<IEnumerable<Contribution?>> Handle(GetContributionsByBillIdQuery query)
    {
        return await contributionRepository.FindByBillIdAsync(query.BillId);
    }

    public async Task<IEnumerable<Contribution?>> Handle(GetContributionsByHouseholdIdQuery query)
    {
        return await contributionRepository.FindByHouseholdIdAsync(query.HouseholdId);
    }

    public async Task<Contribution?> Handle(GetContributionByIdQuery query)
    {
        return await contributionRepository.FindByStringIdAsync(query.Id);
    }
    
}