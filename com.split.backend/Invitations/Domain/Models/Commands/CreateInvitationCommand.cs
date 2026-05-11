namespace com.split.backend.Invitations.Domain.Models.Commands;

public record CreateInvitationCommand(string Email, string HouseholdId, string Description);
