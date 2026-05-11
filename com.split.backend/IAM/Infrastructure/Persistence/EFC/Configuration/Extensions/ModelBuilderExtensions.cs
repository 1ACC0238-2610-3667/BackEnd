using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
        
        builder.Entity<EmailAddress>().HasKey(e => e.Id);
        builder.Entity<EmailAddress>().Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Entity<EmailAddress>().Property(e => e.Address).HasMaxLength(100).IsRequired();
        
        builder.Entity<User>().Property(u => u.Password).IsRequired();
        
        builder.Entity<PersonName>().HasKey(p => p.Id);
        builder.Entity<PersonName>().Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Entity<PersonName>().Property(p => p.FirstName);
        builder.Entity<PersonName>().Property(p => p.LastName);
        
        builder.Entity<User>().Property(u => u.HouseholdId).IsRequired();

        builder.Entity<User>().Property(u => u.Status);
        builder.Entity<User>().Property(u => u.Plan)
            .HasConversion<int>()
            .HasDefaultValue(EPlan.Free);
        builder.Entity<User>().Property(u => u.Photo);
        builder.Entity<User>().Property(u => u.ProfileLockedUntil);
        builder.Entity<User>().Property(u => u.IsNewUser).HasDefaultValue(true);

        builder.Entity<User>().Property(u => u.CreatedDate);
        builder.Entity<User>().Property(u => u.UpdatedDate);
    }


    public static void ApplyUserIncomeConfiguration(this ModelBuilder builder)
    {
        builder.Entity<UserIncome>().HasKey(u => u.Id);
        builder.Entity<UserIncome>().Property(u => u.Id).IsRequired().ValueGeneratedNever();
        builder.Entity<UserIncome>().Property(u => u.UserId).IsRequired();
        builder.Entity<UserIncome>().Property(u =>  u.Income)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        builder.Entity<UserIncome>().Property(u => u.CreatedDate).IsRequired();
        builder.Entity<UserIncome>().Property(u => u.UpdatedDate).IsRequired();
    }
}
