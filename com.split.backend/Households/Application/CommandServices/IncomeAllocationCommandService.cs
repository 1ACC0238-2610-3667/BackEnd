using com.split.backend.Households.Domain.Models.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Domain.Repositories;
using com.split.backend.Households.Domain.Services;
using com.split.backend.Shared.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace com.split.backend.Households.Application.CommandServices;

public class IncomeAllocationCommandService(
    IIncomeAllocationRepository incomeAllocationRepository,
    IHouseHoldRepository houseHoldRepository,
    IUnitOfWork unitOfWork,
    ILogger<IncomeAllocationCommandService> logger) : IIncomeAllocationCommandService
{
    public async Task<IncomeAllocation?> Handle(CreateIncomeAllocationCommand command)
    {
        if(!houseHoldRepository.ExistsById(command.HouseholdId))
            throw new KeyNotFoundException("Household does not exist");
        
        var incomeAllocation = new IncomeAllocation(command);
        try
        {
            await incomeAllocationRepository.AddAsync(incomeAllocation);
            await unitOfWork.CompleteAsync();
            return incomeAllocation;

        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating income allocation for user {UserId} in household {HouseholdId}", command.UserId, command.HouseholdId);
            return null;
        }
    }

    public async Task<IncomeAllocation?> Handle(UpdateIncomeAllocationCommand command)
    {
        var incomeAllocation = await incomeAllocationRepository.FindByStringIdAsync(command.Id);
        if (incomeAllocation == null) return null;

        try
        {
            incomeAllocation.Update(command);

            incomeAllocationRepository.Update(incomeAllocation);
            await unitOfWork.CompleteAsync();
            
            return incomeAllocation;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating income allocation {IncomeAllocationId}", command.Id);
            throw;
        }
    }

    public async Task<bool> Handle(DeleteIncomeAllocationCommand command)
    {
        var incomeAllocation = await incomeAllocationRepository.FindByStringIdAsync(command.Id);
        if(incomeAllocation == null) return false;
        
        try
        {
            incomeAllocationRepository.Remove(incomeAllocation);
            await unitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error deleting income allocation {IncomeAllocationId}", command.Id);
            throw;
        }
    }


    public async Task<IEnumerable<IncomeAllocation>> UpsertAllocationsAsync(IEnumerable<IncomeAllocationUpsertCommand> commands)
    {
        var commandList = commands.ToList();
        var updated = new List<IncomeAllocation>();

        foreach (var group in commandList.GroupBy(c => c.UserId))
        {
            var byUser = (await incomeAllocationRepository.FindByUserIdAsync(group.Key)).ToList();
            foreach (var cmd in group)
            {
                var current = byUser.FirstOrDefault(a => a.HouseholdId == cmd.HouseholdId);
                if (current == null)
                {
                    var newAllocation = new IncomeAllocation(cmd);
                    await incomeAllocationRepository.AddAsync(newAllocation);
                    updated.Add(newAllocation);
                }
                else
                {
                    current.Percentage = cmd.Percentage;
                    current.UpdatedDate = DateTimeOffset.UtcNow;
                    incomeAllocationRepository.Update(current);
                    updated.Add(current);
                }
            }
        }

        await unitOfWork.CompleteAsync();
        return updated;
    }
}
