using com.split.backend.Invitations.Domain.Models.Aggregates;
using com.split.backend.Invitations.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Invitations.Infrastructure.Persistence.EFC.Repositories;

public class InvitationRepository(AppDbContext context) : IInvitationRepository
{
    public async Task AddAsync(Invitation invitation)
    {
        await context.Set<Invitation>().AddAsync(invitation);
    }

    public void Update(Invitation invitation)
    {
        context.Set<Invitation>().Update(invitation);
    }

    public async Task<Invitation?> FindPendingAsync(string email, string householdId)
    {
        return await context.Set<Invitation>()
            .Where(i => i.Email == email && i.HouseholdId == householdId && i.Status == InvitationStatus.Pending)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsPendingAsync(string email, string householdId)
    {
        return await context.Set<Invitation>()
            .AnyAsync(i => i.Email == email && i.HouseholdId == householdId && i.Status == InvitationStatus.Pending);
    }

    public async Task<IEnumerable<Invitation>> FindPendingByHouseholdIdAsync(string householdId)
    {
        return await context.Set<Invitation>()
            .Where(i => i.HouseholdId == householdId && i.Status == InvitationStatus.Pending)
            .ToListAsync();
    }

    public async Task<int> CountPendingByHouseholdIdAsync(string householdId)
    {
        return await context.Set<Invitation>()
            .Where(i => i.HouseholdId == householdId && i.Status == InvitationStatus.Pending)
            .CountAsync();
    }
}
