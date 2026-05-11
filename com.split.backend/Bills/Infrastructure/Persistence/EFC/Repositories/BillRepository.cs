using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Bills.Domain.Repositories;
using com.split.backend.Shared.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Bills.Infrastructure.Persistence.EFC.Repositories;

public class BillRepository(AppDbContext context) : BaseRepository<Bill>(context), IBillRepository
{
    public async Task<Bill?> FindByStringIdAsync(string id)
    {
        return await Context.Set<Bill>()
            .Where(bill => bill.Id == id)
            .FirstOrDefaultAsync();
    }

    public bool ExistsById(string id)
    {
        return Context.Set<Bill>().Any(bill => bill.Id == id);
    }

    public async Task<IEnumerable<Bill?>> FindByHouseholdIdAsync(string houseHoldId)
    {
        return await Context.Set<Bill>()
            .Where(bill => bill.HouseholdId == houseHoldId)
            .ToListAsync();
    }
   
}