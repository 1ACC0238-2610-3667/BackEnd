using com.split.backend.Invitations.Domain.Models.Aggregates;
using com.split.backend.Invitations.Domain.Models.Commands;

namespace com.split.backend.Invitations.Domain.Services;

public interface IInvitationCommandService
{
    Task<Invitation> Handle(CreateInvitationCommand command);
}
