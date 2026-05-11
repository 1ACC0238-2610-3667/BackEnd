using com.split.backend.Contributions.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Contributions.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyContributionsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Contribution>().HasKey(c => c.Id);
        builder.Entity<Contribution>().Property(c => c.Id).IsRequired();
        
        builder.Entity<Contribution>().Property(c => c.BillId).IsRequired();
        builder.Entity<Contribution>().Property(c => c.HouseholdId).IsRequired();
        builder.Entity<Contribution>().Property(c => c.Description);
        builder.Entity<Contribution>().Property(c => c.DeadlineForMembers);
        builder.Entity<Contribution>().Property(c => c.Strategy);
        
    }
    
}