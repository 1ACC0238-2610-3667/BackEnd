using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.split.backend.HouseholdMembers.Domain.Models.Aggregates;
using com.split.backend.HouseholdMembers.Domain.Models.Commands;
using com.split.backend.HouseholdMembers.Domain.Repositories;
using com.split.backend.HouseholdMembers.Domain.Services;
using com.split.backend.HouseholdMembers.Interface.ACL;
using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Domain.Repositories;
using com.split.backend.Households.Domain.Services;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.IAM.Domain.Model.Commands;
using Microsoft.Extensions.Logging;
using IUnitOfWork = com.split.backend.Shared.Domain.Repositories.IUnitOfWork;

namespace com.split.backend.HouseholdMembers.Application.Internal.CommandServices;

public class HouseholdMemberCommandService(
    IHouseholdMemberRepository repository,
    IHouseholdContextFacade householdContextFacade,
    IUserContextFacade userContextFacade,
    IHouseHoldRepository householdRepository,
    IIncomeAllocationCommandService incomeAllocationCommandService,
    IUserIncomeRepository userIncomeRepository,
    IUserIncomeCommandService userIncomeCommandService,
    IUnitOfWork unitOfWork,
    ILogger<HouseholdMemberCommandService> logger
) : IHouseholdMemberCommandService
{
    public async Task<HouseholdMember?> Handle(CreateHouseholdMemberCommand command)
    {
        // Validar que el household existe
        if (!await householdContextFacade.ExistsHouseholdByIdAsync(command.HouseholdId))
            throw new Exception("Household not found");

        // Validar que el usuario existe
        if (!await userContextFacade.ExistsUserByIdAsync(command.UserId))
            throw new Exception("User not found");

        // Validar que no existe ya el miembro
        if (await repository.ExistsByHouseholdIdAndUserIdAsync(command.HouseholdId, command.UserId))
            throw new Exception("Member already exists in this household");

        // Validar cupo (member_count)
        var household = await householdRepository.FindByStringIdAsync(command.HouseholdId);
        if (household == null) throw new Exception("Household not found");
        var currentCount = await repository.CountByHouseholdIdAsync(command.HouseholdId);
        if (currentCount >= household.MemberCount)
            throw new Exception("El household ha alcanzado el numero maximo de miembros permitidos.");

        // Si se está creando como representante, validar que no haya otro representante
        if (command.IsRepresentative)
        {
            var existingRepresentative = await repository.FindRepresentativeByHouseholdIdAsync(command.HouseholdId);
            if (existingRepresentative != null)
                throw new Exception("A representative already exists for this household");
        }

        var member = new HouseholdMember(command.HouseholdId, command.UserId, command.IsRepresentative, command.Income);
        
        try
        {
            await repository.AddAsync(member);
            await unitOfWork.CompleteAsync();
            return member;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating household member for household {HouseholdId} and user {UserId}", command.HouseholdId, command.UserId);
            throw new Exception($"Error creating household member: {e.Message}");
        }
    }

    public async Task<bool> Handle(DeleteHouseholdMemberCommand command)
    {
        var member = await repository.FindByIdAsync(command.Id);
        if (member == null) return false;

        repository.Remove(member);
        await unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<HouseholdMember?> Handle(UpdateHouseholdMemberCommand command)
    {
        var member = await repository.FindByIdAsync(command.Id);
        if (member == null) return null;

        var targetHouseholdId = command.HouseholdId ?? member.HouseholdId;
        var targetUserId = command.UserId ?? member.UserId;

        if (command.IsRepresentative == true && !member.IsRepresentative)
        {
            var existingRepresentative = await repository.FindRepresentativeByHouseholdIdAsync(targetHouseholdId);
            if (existingRepresentative != null && existingRepresentative.Id != member.Id)
                logger.LogWarning("A representative already exists for this household {HouseholdId}", targetHouseholdId);
        }

        member.HouseholdId = targetHouseholdId;
        member.UserId = targetUserId;
        member.UpdateIncome(command.Income, command.IsRepresentative);
        
        repository.Update(member);

        await UpsertUserIncomeAsync(targetUserId, command.Income);

        if (command.Allocations.Any())
            await UpsertAllocationsAsync(targetUserId, targetHouseholdId, command.Allocations);

        await unitOfWork.CompleteAsync();
        return member;
    }

    public async Task<HouseholdMember?> Handle(PromoteToRepresentativeCommand command)
    {
        var member = await repository.FindByIdAsync(command.Id);
        if (member == null) return null;

        // Validar que no haya otro representante
        var existingRepresentative = await repository.FindRepresentativeByHouseholdIdAsync(member.HouseholdId);
        if (existingRepresentative != null && existingRepresentative.Id != member.Id)
            throw new Exception("A representative already exists for this household");

        member.PromoteToRepresentative();
        
        repository.Update(member);
        await unitOfWork.CompleteAsync();
        return member;
    }

    public async Task<HouseholdMember?> Handle(DemoteRepresentativeCommand command)
    {
        var member = await repository.FindByIdAsync(command.Id);
        if (member == null) return null;

        if (!member.IsRepresentative)
            throw new Exception("Member is not a representative");

        member.DemoteRepresentative();
        
        repository.Update(member);
        await unitOfWork.CompleteAsync();
        return member;
    }

    private async Task UpsertUserIncomeAsync(long userId, decimal? income)
    {
        if (!income.HasValue) return;
        
        var existingIncome = await userIncomeRepository.FindByUserIdAsync(userId);
        if (existingIncome == null)
        {
            var createCommand = new CreateUserIncomeCommand(userId, income.Value);
            await userIncomeCommandService.Handle(createCommand);
            return;
        }

        var updateCommand = new UpdateUserIncomeCommand(existingIncome.Id, income.Value);
        await userIncomeCommandService.Handle(updateCommand);
    }

    private async Task UpsertAllocationsAsync(long userId, string fallbackHouseholdId, IEnumerable<IncomeAllocationItem> allocations)
    {
        var allocationCommands = allocations.Select(a =>
            new IncomeAllocationUpsertCommand(
                a.UserId ?? userId,
                a.HouseholdId ?? fallbackHouseholdId,
                a.Percentage));

        await incomeAllocationCommandService.UpsertAllocationsAsync(allocationCommands);
    }

}



