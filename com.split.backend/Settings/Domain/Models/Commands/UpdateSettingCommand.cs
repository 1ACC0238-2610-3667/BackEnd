namespace com.split.backend.Settings.Domain.Models.Commands;

public record UpdateSettingCommand(
    long Id,
    long UserId,
    string Language,
    bool DarkMode,
    bool NotificationEnabled);
