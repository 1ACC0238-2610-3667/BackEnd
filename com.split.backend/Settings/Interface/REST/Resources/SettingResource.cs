namespace com.split.backend.Settings.Interface.REST.Resources;

public record SettingResource(
    long Id,
    long UserId,
    string Language,
    bool DarkMode,
    bool NotificationEnabled,
    DateTime CreatedAt,
    DateTime UpdatedAt);
