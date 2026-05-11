using com.split.backend.Invitations.Domain.Models.Aggregates;
using com.split.backend.Invitations.Domain.Models.Commands;
using com.split.backend.Invitations.Domain.Repositories;
using com.split.backend.Invitations.Domain.Services;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.Invitations.Application.Internal.CommandServices;

public class InvitationCommandService(
    IInvitationRepository invitationRepository,
    com.split.backend.Households.Domain.Repositories.IHouseHoldRepository householdRepository,
    com.split.backend.HouseholdMembers.Domain.Repositories.IHouseholdMemberRepository householdMemberRepository,
    IUnitOfWork unitOfWork) : IInvitationCommandService
{
    public async Task<Invitation> Handle(CreateInvitationCommand command)
    {
        var email = command.Email.Trim().ToLowerInvariant();
        var householdId = command.HouseholdId.Trim();

        // validar existencia de household
        var household = await householdRepository.FindByStringIdAsync(householdId);
        if (household == null)
            throw new Exception("Household not found.");

        // validar cupo por household (miembros actuales + invitaciones pendientes)
        var currentMembers = await householdMemberRepository.CountByHouseholdIdAsync(householdId);
        if (currentMembers >= household.MemberCount)
            throw new Exception("El household ha alcanzado el número máximo de miembros permitidos.");

        var pendingInvites = await invitationRepository.CountPendingByHouseholdIdAsync(householdId);
        if ((currentMembers + pendingInvites) >= household.MemberCount)
            throw new Exception("El household ha alcanzado el número máximo de miembros permitidos.");

        if (await invitationRepository.ExistsPendingAsync(email, householdId))
            throw new Exception("A pending invitation already exists for this email and household.");

        var invitation = new Invitation
        {
            Email = email,
            HouseholdId = householdId,
            Description = command.Description ?? string.Empty
        };

        await invitationRepository.AddAsync(invitation);
        await unitOfWork.CompleteAsync();
        return invitation;
    }
}
