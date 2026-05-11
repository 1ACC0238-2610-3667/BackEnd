using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.IAM.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Households.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyHouseHoldConfiguration(this ModelBuilder builder)
    {
        builder.Entity<HouseHold>().HasKey(p => p.Id);
        builder.Entity<HouseHold>().Property(p => p.Id).IsRequired();
        builder.Entity<HouseHold>().Property(p => p.Name);
        builder.Entity<HouseHold>().Property(p => p.Currency).IsRequired();
        builder.Entity<HouseHold>().Property(p => p.RepresentativeId).IsRequired();
    }

    public static void ApplyIncomeAllocationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<IncomeAllocation>().HasKey(p =>p.Id);
        builder.Entity<IncomeAllocation>().Property(p => p.Id).IsRequired().ValueGeneratedNever();
        builder.Entity<IncomeAllocation>().Property(p => p.UserId).IsRequired();
        builder.Entity<IncomeAllocation>().Property(p => p.HouseholdId).IsRequired();
        builder.Entity<IncomeAllocation>().Property(p => p.Percentage)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        builder.Entity<IncomeAllocation>().Property(p => p.CreatedDate).IsRequired();
        builder.Entity<IncomeAllocation>().Property(p => p.UpdatedDate).IsRequired();
    }
    
}
