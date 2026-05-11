using com.split.backend.Settings.Domain.Models.Commands;
using com.split.backend.Settings.Interface.REST.Resources;

namespace com.split.backend.Settings.Interface.REST.Transform;

public static class CreateSettingCommandFromResourceAssembler
{
    public static CreateSettingCommand ToCommandFromResource(CreateSettingResource resource)
    {
        return new CreateSettingCommand(
            resource.UserId,
            resource.Language,
            resource.DarkMode,
            resource.NotificationEnabled);
    }
}
