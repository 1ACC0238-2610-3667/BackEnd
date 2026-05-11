using com.split.backend.MemberContributions.Domain.Model.Aggregates;
using com.split.backend.MemberContributions.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.MemberContributions.Infrastructure.Persistence.EFC.Repositories;

public class MemberContributionRepository(AppDbContext context) : BaseRepository<MemberContribution>(context), IMemberContributionRepository
{
   public async Task<MemberContribution?> FindByStringIdAsync(string id)
   {
      return await Context.Set<MemberContribution>().FirstOrDefaultAsync(p => p.Id.Equals(id));
   }

   public bool ExistsById(string id)
   {
      return Context.Set<MemberContribution>().Any(p => p.Id.Equals(id));
   }

   public async Task<IEnumerable<MemberContribution?>> FindByContributionIdAsync(string contributionId)
   {
      return await Context.Set<MemberContribution>().Where(p => p.Id.Equals(contributionId)).ToListAsync();
   }

   public async Task<IEnumerable<MemberContribution?>> FindByMemberIdAsync(string memberId)
   {
      return await Context.Set<MemberContribution>().Where(p => p.MemberId.Equals(memberId)).ToListAsync();
   }
}