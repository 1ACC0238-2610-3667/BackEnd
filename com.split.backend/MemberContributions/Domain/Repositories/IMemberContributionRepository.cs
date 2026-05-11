using com.split.backend.MemberContributions.Domain.Model.Aggregates;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.MemberContributions.Domain.Repositories;

public interface IMemberContributionRepository : IBaseRepository<MemberContribution>
{
    Task<MemberContribution?> FindByStringIdAsync(string id);
    
    bool ExistsById(string id);
    
    Task<IEnumerable<MemberContribution?>> FindByContributionIdAsync(string contributionId);
    
    Task<IEnumerable<MemberContribution?>> FindByMemberIdAsync(string memberId);
}