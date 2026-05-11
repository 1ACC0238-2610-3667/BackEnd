using com.split.backend.MemberContributions.Domain.Model.Aggregates;
using com.split.backend.MemberContributions.Domain.Model.Commands;

namespace com.split.backend.MemberContributions.Domain.Services;

public interface IMemberContributionCommandService
{
    public Task<MemberContribution?> Handle(CreateMemberContributionCommand command);
    
    public Task<bool> Handle(DeleteMemberContributionCommand command);
    
    public Task<MemberContribution?> Handle(UpdateMemberContributionAmountCommand command);
    
}