using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.Commands;
using com.split.backend.IAM.Domain.Model.Events;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.Shared.Domain.Repositories;
using Cortex.Mediator;
using Microsoft.Extensions.Logging;

namespace com.split.backend.IAM.Application.Internal.CommandServices;

public class UserIncomeCommandServices(IUserIncomeRepository userIncomeRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IMediator domainEventPublisher,
    ILogger<UserIncomeCommandServices> logger) : IUserIncomeCommandService
{
    public async Task<UserIncome?> Handle(CreateUserIncomeCommand command)
    {
        var user = await userRepository.FindByIdAsync((int)command.UserId);
        if(user == null) 
            throw new ArgumentException("User does not exist");
        
        var userIncome = new UserIncome(command);

        if (userIncomeRepository.ExistsByUserId(command.UserId)) 
            throw new Exception("User already exists");

        try
        {
            await userIncomeRepository.AddAsync(userIncome);
            await unitOfWork.CompleteAsync();

            await domainEventPublisher.PublishAsync(new UserIncomeCreatedEvent(userIncome.Id));
            
            return userIncome;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating user income for user {UserId}", command.UserId);
            throw;
        }
    }


    public async Task<UserIncome?> Handle(UpdateUserIncomeCommand command)
    {
        var userIncome = await userIncomeRepository.FindByStringIdAsync(command.Id);
        if(userIncome == null) throw new ArgumentException("User does not exist");
        
        try
        {
            userIncome.Update(command);
            userIncomeRepository.Update(userIncome);
            await unitOfWork.CompleteAsync();
            
            await domainEventPublisher.PublishAsync(new UserIncomeUpdatedEvent(userIncome.Id));
            
            return userIncome;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating user income {UserIncomeId} for user {UserId}", command.Id, userIncome.UserId);
            throw;
        }
    }
    
    
}
