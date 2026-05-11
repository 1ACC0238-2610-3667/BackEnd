using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Households.Infrastructure.Persistence.EFC.Repositories;

public class HouseHoldRepository(AppDbContext context) : BaseRepository<HouseHold>(context), IHouseHoldRepository
{
    public async Task<HouseHold?> FindByStringIdAsync(string id)
    {
        return await Context.Set<HouseHold>().FirstOrDefaultAsync(p => p.Id == id);
    }

    public bool ExistsById(string Id)
    {
        return Context.Set<HouseHold>().Any(household => household.Id.Equals(Id));
    }

    public async Task<IEnumerable<HouseHold?>> FindByRepresentativeIdAsync(long representativeId)
    {
        return await Context.Set<HouseHold>()
            .Where(household => household.RepresentativeId == representativeId).ToListAsync();
    }
    
    
}