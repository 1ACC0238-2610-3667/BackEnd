namespace com.split.backend.Invitations.Interface.REST.Resources;

public record CreateInvitationResource(string Email, string HouseholdId, string Description);
