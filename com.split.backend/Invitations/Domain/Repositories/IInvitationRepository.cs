using com.split.backend.Invitations.Domain.Models.Aggregates;

namespace com.split.backend.Invitations.Domain.Repositories;

public interface IInvitationRepository
{
    Task AddAsync(Invitation invitation);
    void Update(Invitation invitation);
    Task<Invitation?> FindPendingAsync(string email, string householdId);
    Task<bool> ExistsPendingAsync(string email, string householdId);
    Task<IEnumerable<Invitation>> FindPendingByHouseholdIdAsync(string householdId);
    Task<int> CountPendingByHouseholdIdAsync(string householdId);
}
