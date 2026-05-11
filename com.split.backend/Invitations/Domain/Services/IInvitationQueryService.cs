using com.split.backend.Invitations.Domain.Models.Aggregates;

namespace com.split.backend.Invitations.Domain.Services;

public interface IInvitationQueryService
{
    Task<Invitation?> FindPendingAsync(string email, string householdId);
}
