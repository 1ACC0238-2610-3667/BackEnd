using com.split.backend.MemberContributions.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.MemberContributions.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyMemberContributionConfiguration(this ModelBuilder builder)
    {
        builder.Entity<MemberContribution>().HasKey(p => p.Id);
        builder.Entity<MemberContribution>().Property(p => p.Id).IsRequired();
        builder.Entity<MemberContribution>().Property(p => p.ContributionId).IsRequired();
        builder.Entity<MemberContribution>().Property(p => p.MemberId).IsRequired();
        builder.Entity<MemberContribution>().Property(p => p.Amount).IsRequired();
        builder.Entity<MemberContribution>().Property(p => p.Status);
        builder.Entity<MemberContribution>().Property(p => p.PayedAt);
    }
    
}