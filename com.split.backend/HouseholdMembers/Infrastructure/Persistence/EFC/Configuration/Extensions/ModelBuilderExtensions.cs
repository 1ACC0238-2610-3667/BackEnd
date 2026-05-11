using com.split.backend.HouseholdMembers.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.HouseholdMembers.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyHouseholdMemberConfiguration(this ModelBuilder builder)
    {
        builder.Entity<HouseholdMember>().HasKey(m => m.Id);
        builder.Entity<HouseholdMember>().Property(m => m.Id).ValueGeneratedNever();
        
        builder.Entity<HouseholdMember>().Property(m => m.HouseholdId).IsRequired();
        builder.Entity<HouseholdMember>().Property(m => m.UserId).IsRequired();
        builder.Entity<HouseholdMember>().Property(m => m.IsRepresentative).IsRequired();
        builder.Entity<HouseholdMember>().Property(m => m.Income)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        builder.Entity<HouseholdMember>().Property(m => m.JoinedAt).IsRequired();
        builder.Entity<HouseholdMember>().Property(m => m.CreatedAt).IsRequired();
        builder.Entity<HouseholdMember>().Property(m => m.UpdatedAt).IsRequired();
        
        builder.Entity<HouseholdMember>()
            .HasIndex(m => new { m.HouseholdId, m.UserId })
            .IsUnique();
    }
}

