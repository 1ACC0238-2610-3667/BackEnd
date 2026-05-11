using com.split.backend.Invitations.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Invitations.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyInvitationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Invitation>().HasKey(i => i.Id);
        builder.Entity<Invitation>().Property(i => i.Id).ValueGeneratedOnAdd();
        builder.Entity<Invitation>().Property(i => i.Email).IsRequired();
        builder.Entity<Invitation>().Property(i => i.HouseholdId).IsRequired();
        builder.Entity<Invitation>().Property(i => i.Description);
        builder.Entity<Invitation>().Property(i => i.Status).IsRequired();
        builder.Entity<Invitation>().Property(i => i.Token).IsRequired();
        builder.Entity<Invitation>().Property(i => i.ExpiresAt).IsRequired();
        builder.Entity<Invitation>().Property(i => i.CreatedDate);
        builder.Entity<Invitation>().Property(i => i.UpdatedDate);
    }
}
