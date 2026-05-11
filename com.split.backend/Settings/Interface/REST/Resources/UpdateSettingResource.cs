namespace com.split.backend.Settings.Interface.REST.Resources;

public record UpdateSettingResource(
    long UserId,
    string Language,
    bool DarkMode,
    bool NotificationEnabled);
