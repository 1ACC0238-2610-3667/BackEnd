using com.split.backend.Settings.Domain.Models.Aggregates;
using com.split.backend.Settings.Domain.Models.Queries;

namespace com.split.backend.Settings.Domain.Services;

public interface ISettingsQueryService
{
    Task<Setting?> Handle(GetSettingByUserIdQuery query);
}
