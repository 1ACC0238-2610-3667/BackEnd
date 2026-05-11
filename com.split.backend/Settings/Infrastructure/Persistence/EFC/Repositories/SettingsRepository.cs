using com.split.backend.Settings.Domain.Models.Aggregates;
using com.split.backend.Settings.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Settings.Infrastructure.Persistence.EFC.Repositories;

public class SettingsRepository(AppDbContext context) : BaseRepository<Setting>(context), ISettingsRepository
{
    public async Task<Setting?> FindByIdAsync(long id)
    {
        return await Context.Set<Setting>()
            .FirstOrDefaultAsync(setting => setting.Id == id);
    }

    public async Task<Setting?> FindByUserIdAsync(long userId)
    {
        var normalizedUserId = checked((int)userId);
        return await Context.Set<Setting>()
            .FirstOrDefaultAsync(setting => setting.UserId == normalizedUserId);
    }

    public async Task<bool> ExistsByUserIdAsync(long userId)
    {
        var normalizedUserId = checked((int)userId);
        return await Context.Set<Setting>()
            .AnyAsync(setting => setting.UserId == normalizedUserId);
    }
}
