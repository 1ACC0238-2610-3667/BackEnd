using com.split.backend.Contributions.Domain.Model.Aggregates;
using com.split.backend.Contributions.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Contributions.Infrastructure.Persistence.EFC.Repositories;

public class ContributionRepository(AppDbContext context) : BaseRepository<Contribution>(context), IContributionRepository
{
    public async Task<Contribution?> FindByStringIdAsync(string id)
    {
        return await Context.Set<Contribution>()
            .Where(contribution => contribution.Id == id)
            .FirstOrDefaultAsync();
    }
    
    public bool ExistsById(string id)
    {
        return Context.Set<Contribution>().Any(contribution => contribution.Id == id);
    }

    public async Task<IEnumerable<Contribution?>> FindByBillIdAsync(string billId)
    {
        return await Context.Set<Contribution>()
            .Where(contribution => contribution.BillId == billId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Contribution?>> FindByHouseholdIdAsync(string householdId)
    {
        return await Context.Set<Contribution>()
            .Where(contribution => contribution.HouseholdId == householdId)
            .ToListAsync();
    }
    
}