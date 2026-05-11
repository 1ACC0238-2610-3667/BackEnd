using com.split.backend.HouseholdMembers.Domain.Models.Aggregates;
using com.split.backend.HouseholdMembers.Domain.Repositories;
using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Domain.Repositories;
using com.split.backend.Households.Domain.Services;
using com.split.backend.IAM.Domain.Model.ValueObjects;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.Shared.Domain.Repositories;

namespace com.split.backend.Households.Application.CommandServices;

public class HouseHoldCommandService(
    IHouseHoldRepository houseHoldRepository,
    IUserRepository userRepository,
    IHouseholdMemberRepository householdMemberRepository,
    IUnitOfWork unitOfWork
) : IHouseHoldCommandService
{
    public async Task<HouseHold?> Handle(CreateHouseholdCommand command)
    {
        // Validate representative exists
        var rep = await userRepository.FindByIdAsync((int)command.RepresentativeId);
        if (rep is null) throw new InvalidOperationException("Representative not found.");

        var repPlan = rep.Plan ?? EPlan.Free; // assume Free when undefined

        if (repPlan == EPlan.Free)
        {
            var existing = await houseHoldRepository.FindByRepresentativeIdAsync(command.RepresentativeId);
            if (existing.Any())
                throw new InvalidOperationException("Free plan allows only one household. Delete the existing one first.");
            if (command.MemberCount > 3)
                throw new InvalidOperationException("Free plan allows up to 3 members.");
        }
        if (command.MemberCount < 1)
            throw new InvalidOperationException("Member count must be at least 1.");

        var household = new HouseHold(command);
        try
        {
            await houseHoldRepository.AddAsync(household);
            await unitOfWork.CompleteAsync();

            // Auto-create representative as household member
            var currentCount = await householdMemberRepository.CountByHouseholdIdAsync(household.Id);
            if (currentCount >= household.MemberCount)
                throw new InvalidOperationException("El household ha alcanzado el número máximo de miembros permitidos.");

            if (!await householdMemberRepository.ExistsByHouseholdIdAndUserIdAsync(household.Id, (int)rep.Id))
            {
                var repMember = new HouseholdMember(household.Id, (int)rep.Id, true, 0m);
                await householdMemberRepository.AddAsync(repMember);
            }

            await unitOfWork.CompleteAsync();
            return household;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<HouseHold?> Handle(UpdateHouseHoldCommand command)
    {
        var household = await houseHoldRepository.FindByStringIdAsync(command.Id);
        if (household == null) return null;

        var rep = await userRepository.FindByIdAsync((int)household.RepresentativeId);
        if (rep?.Plan == EPlan.Free && command.MemberCount > 3)
            throw new InvalidOperationException("Free plan allows up to 3 members.");
        if (command.MemberCount < 1)
            throw new InvalidOperationException("Member count must be at least 1.");

        household.UpdateHouseHold(command);
        
        houseHoldRepository.Update(household);
        
        await unitOfWork.CompleteAsync();

        return household;
    }

    public async Task<bool> Handle(DeleteHouseHoldCommand command)
    {
        var household = await houseHoldRepository.FindByStringIdAsync(command.Id);
        if (household == null) return false;
        
        houseHoldRepository.Remove(household);
        await unitOfWork.CompleteAsync();

        return true;
    }
}
