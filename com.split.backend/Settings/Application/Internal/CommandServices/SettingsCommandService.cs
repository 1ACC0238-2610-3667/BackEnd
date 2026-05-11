using com.split.backend.Settings.Application.Internal.OutboundServices.ACL;
using com.split.backend.Settings.Domain.Models.Aggregates;
using com.split.backend.Settings.Domain.Models.Commands;
using com.split.backend.Settings.Domain.Repositories;
using com.split.backend.Settings.Domain.Services;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.Settings.Application.Internal.CommandServices;

public class SettingsCommandService(
    ISettingsRepository settingsRepository,
    IUnitOfWork unitOfWork,
    IExternalIamService externalIamService)
    : ISettingsCommandService
{
    public async Task<Setting?> Handle(CreateSettingCommand command)
    {
        await externalIamService.EnsureUserExists(command.UserId);

        var existing = await settingsRepository.FindByUserIdAsync(command.UserId);
        if (existing is not null) return existing;

        var setting = new Setting(command);
        await settingsRepository.AddAsync(setting);
        await unitOfWork.CompleteAsync();
        return setting;
    }

    public async Task<Setting?> Handle(UpdateSettingCommand command)
    {
        await externalIamService.EnsureUserExists(command.UserId);

        var existing = await settingsRepository.FindByIdAsync(command.Id);
        if (existing is null) return null;
        if (existing.UserId != command.UserId) return null;

        existing.Update(command);
        settingsRepository.Update(existing);

        await unitOfWork.CompleteAsync();
        return existing;
    }
}
