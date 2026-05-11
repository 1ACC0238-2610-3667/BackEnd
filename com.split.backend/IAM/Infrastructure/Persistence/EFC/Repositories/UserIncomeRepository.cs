using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserIncomeRepository(AppDbContext context) : BaseRepository<UserIncome>(context), IUserIncomeRepository
{
    public async Task<UserIncome?> FindByStringIdAsync(string id)
    {
        return await Context.Set<UserIncome>()
            .Where(userIncome => userIncome.Id == id)
            .FirstOrDefaultAsync();
    }

    public bool ExistsByUserId(long userId)
    {
        return Context.Set<UserIncome>().Any(userIncome => userIncome.UserId == userId);
    }

    public async Task<UserIncome?> FindByUserIdAsync(long userId)
    {
        return  await Context.Set<UserIncome>().FirstOrDefaultAsync(userIncome => userIncome.UserId == userId);
    }
    
}