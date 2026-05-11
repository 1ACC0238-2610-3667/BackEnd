using com.split.backend.Settings.Domain.Models.Aggregates;
using com.split.backend.Settings.Domain.Models.Queries;
using com.split.backend.Settings.Domain.Repositories;
using com.split.backend.Settings.Domain.Services;

namespace com.split.backend.Settings.Application.Internal.QueryServices;

public class SettingsQueryService(ISettingsRepository settingsRepository) : ISettingsQueryService
{
    public async Task<Setting?> Handle(GetSettingByUserIdQuery query)
    {
        return await settingsRepository.FindByUserIdAsync(query.UserId);
    }
}
