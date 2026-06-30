using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public new async Task<User?> FindByIdAsync(int id)
    {
        return await Context.Set<User>()
            .Include(u => u.Email)
            .Include(u => u.PersonName) // <--- Carga el Nombre
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await Context.Set<User>()
            .Include(u => u.Email)      // <--- Carga el Email para evitar errores en memoria
            .Include(u => u.PersonName) // <--- Carga el Nombre para el Dashboard
            .FirstOrDefaultAsync(u => u.Email.Address == email);
    }

    public bool ExistsByEmail(string email)
    {
        return Context.Set<User>().Any(u => u.Email.Address == email);
    }

    public async Task<User?> FindByHouseHoldIdAsync(string houseHoldId)
    {
        return await Context.Set<User>()
            .Include(u => u.Email)
            .Include(u => u.PersonName)
            .FirstOrDefaultAsync(u => u.HouseholdId == houseHoldId);
    }

    public new async Task<IEnumerable<User>> ListAsync()
    {
        return await Context.Set<User>()
            .Include(u => u.Email)
            .Include(u => u.PersonName)
            .ToListAsync();
    }
}