using com.split.backend.MemberContributions.Domain.Model.Aggregates;
using com.split.backend.MemberContributions.Domain.Model.Queries;

namespace com.split.backend.MemberContributions.Domain.Services;

public interface IMemberContributionQueryService
{
    Task<IEnumerable<MemberContribution?>> Handle(GetAllMemberContributionsQuery query);
    Task<IEnumerable<MemberContribution?>> Handle(GetMemberContributionsByContributionIdQuery query);
    Task<IEnumerable<MemberContribution?>> Handle(GetMemberContributionsByMemberIdQuery query);
    
}