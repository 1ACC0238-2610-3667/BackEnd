using com.split.backend.IAM.Application.Internal.OutboundServices;
using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.Commands;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.IAM.Domain.Model.ValueObjects;
using com.split.backend.Invitations.Domain.Repositories;
using com.split.backend.Invitations.Domain.Models.Aggregates;
using com.split.backend.HouseholdMembers.Domain.Repositories;
using com.split.backend.Households.Domain.Repositories;
using Role = com.split.backend.Households.Domain.Models.ValueObjects.Role;
using IUnitOfWork = com.split.backend.Shared.Domain.Repositories.IUnitOfWork;

namespace com.split.backend.IAM.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IInvitationRepository invitationRepository,
    IHouseholdMemberRepository householdMemberRepository,
    IHouseHoldRepository houseHoldRepository,
    IUnitOfWork unitOfWork
    ) : IUserCommandService
{
    public async Task<(User user, string token, bool wasNewUser)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByEmailAsync(command.EmailAddress);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.Password))
            throw new Exception("Invalid email or password");

        var wasNewUser = user.IsNewUser ?? false;
        
        if (wasNewUser)
        {
            user.IsNewUser = false;
            userRepository.Update(user);
            await unitOfWork.CompleteAsync();
        }

        var token = tokenService.GenerateToken(user);
        return (user, token, wasNewUser);
    }

    public async Task Handle(SignUpCommand command)
    {
        if(userRepository.ExistsByEmail(command.EmailAddress))
            throw new Exception("Email already exists");

        var hashedPassword = hashingService.HashPassword(command.Password);
        if (!Enum.TryParse<Role>(command.Role, true, out var role))
            throw new Exception("Invalid role");

        if (role == Role.Member && !string.IsNullOrWhiteSpace(command.HouseholdId))
        {
            // Validar invitación si existe, pero no bloquear el alta de miembro si la invitación no es requerida
            var invitation = await invitationRepository.FindPendingAsync(command.EmailAddress, command.HouseholdId);
            if (invitation != null && invitation.ExpiresAt < DateTime.UtcNow)
                throw new Exception("Invitation expired.");

            // validar household existe y cupo
            var household = await houseHoldRepository.FindByStringIdAsync(command.HouseholdId);
            if (household == null) throw new Exception("Household not found.");
            var currentMembers = await householdMemberRepository.CountByHouseholdIdAsync(command.HouseholdId);
            var pendingInvites = await invitationRepository.CountPendingByHouseholdIdAsync(command.HouseholdId);
            // Si hay invitaci�n para este email/household, descuenta 1 de los pendientes para permitir su alta
            var pendingForLimit = Math.Max(0, pendingInvites - (invitation != null ? 1 : 0));
            if (household.MemberCount > 0 && (currentMembers + pendingForLimit) >= household.MemberCount)
                throw new Exception("Household member limit reached.");

            var user = new User(command,  hashedPassword)
            {
                IsNewUser = true,
                Plan = Enum.IsDefined(typeof(EPlan), command.Plan) ? (EPlan)command.Plan : EPlan.Free
            };

            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();

            // Crear el registro en household_member siempre que el usuario se una a un household
            var member = new HouseholdMembers.Domain.Models.Aggregates.HouseholdMember(command.HouseholdId, user.Id, false);
            await householdMemberRepository.AddAsync(member);

            // Si había invitación pendiente, marcarla como aceptada
            if (invitation != null)
            {
                invitation.Status = InvitationStatus.Accepted;
                invitation.UpdatedDate = DateTime.UtcNow;
                invitationRepository.Update(invitation);
            }

            await unitOfWork.CompleteAsync();
            return;
        }

        var newUser = new User(command,  hashedPassword)
        {
            IsNewUser = true,
            Plan = Enum.IsDefined(typeof(EPlan), command.Plan) ? (EPlan)command.Plan : EPlan.Free
        };
        await userRepository.AddAsync(newUser);
        await unitOfWork.CompleteAsync();
    }

    public async Task<User?> Handle(UpdateUserCommand command)
    {
        var user = await userRepository.FindByEmailAsync(command.EmailAddress);
        if (user == null) return null;

        user.UpdateUsername(command.Username);
        
        userRepository.Update(user);
        
        await unitOfWork.CompleteAsync();

        return user;
    }

    public async Task<bool> Handle(DeleteUserCommand command)
    {
        var user = await userRepository.FindByEmailAsync(command.EmailAddress);
        if(user == null) return false;
        
        userRepository.Remove(user);
        await unitOfWork.CompleteAsync();

        return true;
    }
}
