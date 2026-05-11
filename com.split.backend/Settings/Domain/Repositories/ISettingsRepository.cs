using com.split.backend.Settings.Domain.Models.Aggregates;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.Settings.Domain.Repositories;

public interface ISettingsRepository : IBaseRepository<Setting>
{
    Task<Setting?> FindByIdAsync(long id);

    Task<Setting?> FindByUserIdAsync(long userId);

    Task<bool> ExistsByUserIdAsync(long userId);
}
