using com.split.backend.MemberContributions.Domain.Model.Aggregates;
using com.split.backend.MemberContributions.Domain.Model.Queries;
using com.split.backend.MemberContributions.Domain.Repositories;
using com.split.backend.MemberContributions.Domain.Services;
using K4os.Compression.LZ4.Internal;

namespace com.split.backend.MemberContributions.Application.Internal.QueryServices;

public class MemberContributionQueryServices(IMemberContributionRepository memberContributionRepository) : IMemberContributionQueryService
{
   public async Task<IEnumerable<MemberContribution?>> Handle(GetAllMemberContributionsQuery query)
   {
      return await memberContributionRepository.ListAsync();
   }

   public async Task<IEnumerable<MemberContribution?>> Handle(GetMemberContributionsByContributionIdQuery query)
   {
      return await memberContributionRepository.FindByContributionIdAsync(query.ContributionId);
   }

   public async Task<IEnumerable<MemberContribution?>> Handle(GetMemberContributionsByMemberIdQuery query)
   {
      return await memberContributionRepository.FindByMemberIdAsync(query.MemberId);
   }
   
    
}