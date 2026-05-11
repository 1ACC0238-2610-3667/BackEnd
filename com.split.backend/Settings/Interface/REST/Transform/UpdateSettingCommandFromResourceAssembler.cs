using com.split.backend.Settings.Domain.Models.Commands;
using com.split.backend.Settings.Interface.REST.Resources;

namespace com.split.backend.Settings.Interface.REST.Transform;

public static class UpdateSettingCommandFromResourceAssembler
{
    public static UpdateSettingCommand ToCommandFromResource(long id, UpdateSettingResource resource)
    {
        return new UpdateSettingCommand(
            id,
            resource.UserId,
            resource.Language,
            resource.DarkMode,
            resource.NotificationEnabled);
    }
}
