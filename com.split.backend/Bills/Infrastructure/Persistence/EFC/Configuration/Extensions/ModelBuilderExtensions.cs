using Microsoft.EntityFrameworkCore;
using com.split.backend.Bills.Domain.Models.Aggregates;

namespace com.split.backend.Bills.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyBillsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Bill>().HasKey(b => b.Id);
        builder.Entity<Bill>().Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Entity<Bill>().Property(b => b.HouseholdId).IsRequired();
        builder.Entity<Bill>().Property(b => b.Description);
        builder.Entity<Bill>().Property(b => b.Amount).IsRequired();
        builder.Entity<Bill>().Property(b => b.CreatedBy).IsRequired();
        builder.Entity<Bill>().Property(b => b.PaymentDate).IsRequired();

        builder.Entity<Bill>().Property(b => b.CreatedDate);
        builder.Entity<Bill>().Property(b => b.UpdatedDate);

    }
}