using com.split.backend.Settings.Domain.Models.Aggregates;
using com.split.backend.Settings.Domain.Models.Commands;

namespace com.split.backend.Settings.Domain.Services;

public interface ISettingsCommandService
{
    Task<Setting?> Handle(CreateSettingCommand command);

    Task<Setting?> Handle(UpdateSettingCommand command);
}
