using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.Settings.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Settings.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySettingsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Setting>(entity =>
        {
            entity.ToTable("settings");
            entity.HasKey(setting => setting.Id);
            entity.Property(setting => setting.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.Property(setting => setting.UserId)
                .IsRequired();
            entity.HasIndex(setting => setting.UserId)
                .IsUnique();
            entity.Property(setting => setting.Language)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(setting => setting.DarkMode)
                .IsRequired();
            entity.Property(setting => setting.NotificationEnabled)
                .IsRequired();
            entity.Property(setting => setting.CreatedAt)
                .IsRequired();
            entity.Property(setting => setting.UpdatedAt)
                .IsRequired();
            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(setting => setting.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
