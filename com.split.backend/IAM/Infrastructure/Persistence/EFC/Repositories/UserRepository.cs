using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email)
    {
        return await Context.Set<User>()
            .Where(user => user.Email.Address == email)
            .FirstOrDefaultAsync();
    }

    public bool ExistsByEmail(string email)
    {
        return Context.Set<User>().Any(user => user.Email.Address.Equals(email));
    }

    public async Task<User?> FindByHouseHoldIdAsync(string houseHoldId)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.HouseholdId.Equals(houseHoldId));

    }

    public new async Task<IEnumerable<User>> ListAsync()
    {
        return await Context.Set<User>()
            .Include(user => user.Email)
            .ToListAsync();
    }
}