using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Households.Infrastructure.Persistence.EFC.Repositories;

public class IncomeAllocationRepository(AppDbContext context) : BaseRepository<IncomeAllocation>(context) , IIncomeAllocationRepository
{
    public async Task<IncomeAllocation?> FindByStringIdAsync(string id)
    {
        return await Context.Set<IncomeAllocation>().FirstOrDefaultAsync(ia => ia.Id == id);
    }

    public bool ExistsById(string id)
    {
        return Context.Set<IncomeAllocation>().Any(ia => ia.Id == id);
    }

    public async Task<IEnumerable<IncomeAllocation?>> FindByHouseholdIdAsync(string householdId)
    {
        return await Context.Set<IncomeAllocation>().Where(ia => ia.HouseholdId == householdId).ToListAsync();
    }

    public async Task<IEnumerable<IncomeAllocation>> FindByUserIdAsync(long userId)
    {
        return await Context.Set<IncomeAllocation>().Where(ia => ia.UserId == userId).ToListAsync();
    }
    
}