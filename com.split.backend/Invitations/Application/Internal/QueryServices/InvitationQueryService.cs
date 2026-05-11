using com.split.backend.Invitations.Domain.Models.Aggregates;
using com.split.backend.Invitations.Domain.Repositories;
using com.split.backend.Invitations.Domain.Services;

namespace com.split.backend.Invitations.Application.Internal.QueryServices;

public class InvitationQueryService(IInvitationRepository repository) : IInvitationQueryService
{
    public async Task<Invitation?> FindPendingAsync(string email, string householdId)
    {
        return await repository.FindPendingAsync(email.Trim().ToLowerInvariant(), householdId.Trim());
    }
}
