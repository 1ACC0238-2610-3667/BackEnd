namespace com.split.backend.Invitations.Interface.REST.Resources;

public record InvitationResource(
    int Id,
    string Email,
    string HouseholdId,
    string Description,
    string Status,
    string Token,
    DateTime ExpiresAt);
