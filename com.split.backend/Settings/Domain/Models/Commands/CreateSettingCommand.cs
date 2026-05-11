namespace com.split.backend.Settings.Domain.Models.Commands;

public record CreateSettingCommand(
    long UserId,
    string Language,
    bool DarkMode,
    bool NotificationEnabled);
