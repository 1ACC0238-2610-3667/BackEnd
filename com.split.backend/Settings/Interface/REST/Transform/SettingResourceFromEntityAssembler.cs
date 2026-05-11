using com.split.backend.Settings.Domain.Models.Aggregates;
using com.split.backend.Settings.Interface.REST.Resources;

namespace com.split.backend.Settings.Interface.REST.Transform;

public static class SettingResourceFromEntityAssembler
{
    public static SettingResource ToResourceFromEntity(Setting setting)
    {
        return new SettingResource(
            setting.Id,
            setting.UserId,
            setting.Language,
            setting.DarkMode,
            setting.NotificationEnabled,
            setting.CreatedAt,
            setting.UpdatedAt);
    }
}
