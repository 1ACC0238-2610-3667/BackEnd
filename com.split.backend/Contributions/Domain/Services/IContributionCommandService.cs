using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Bills.Domain.Models.Commands;
using com.split.backend.Contributions.Domain.Model.Aggregates;
using com.split.backend.Contributions.Domain.Model.Commands;

namespace com.split.backend.Contributions.Domain.Services;

public interface IContributionCommandService
{
    Task<Contribution?> Handle(CreateContributionCommand command);
    Task<Contribution?> Handle(UpdateContributionCommand command);
    Task<bool> Handle(DeleteContributionCommand command);
}