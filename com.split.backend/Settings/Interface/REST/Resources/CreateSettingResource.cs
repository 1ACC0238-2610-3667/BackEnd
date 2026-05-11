namespace com.split.backend.Settings.Interface.REST.Resources;

public record CreateSettingResource(
    long UserId,
    string Language,
    bool DarkMode,
    bool NotificationEnabled);
