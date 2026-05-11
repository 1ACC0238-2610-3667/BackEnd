using com.split.backend.Contributions.Domain.Model.Aggregates;
using com.split.backend.Contributions.Domain.Model.Commands;
using com.split.backend.Contributions.Domain.Model.Events;
using com.split.backend.Contributions.Domain.Repositories;
using com.split.backend.Contributions.Domain.Services;
using Cortex.Mediator;
using Cortex.Mediator.Infrastructure;
using IUnitOfWork = com.split.backend.Shared.Domain.Repositories.IUnitOfWork;

namespace com.split.backend.Contributions.Application.Internal.CommandServices;

public class ContributionCommandService(IContributionRepository contributionRepository, IUnitOfWork unitOfWork,
    IMediator domainEventPublisher) : IContributionCommandService
{
    public async Task<Contribution?> Handle(CreateContributionCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.BillId) || string.IsNullOrWhiteSpace(command.HouseholdId))
            throw new ArgumentException("BillId and HouseholdId are required");

        var contribution = new Contribution(command);
        
        if(contributionRepository.ExistsById(contribution.Id)) throw new Exception("Contribution already exists");
        
        await contributionRepository.AddAsync(contribution);
        await unitOfWork.CompleteAsync();


        await domainEventPublisher.PublishAsync(new ContributionCreatedEvent(contribution.Id));


        return contribution;
    }


    public async Task<Contribution?> Handle(UpdateContributionCommand command)
    {
        var contribution = await contributionRepository.FindByStringIdAsync(command.Id);
        if(contribution == null) throw new Exception("Contribution not found");

        contribution.Update(command);

        contributionRepository.Update(contribution);
        
        await unitOfWork.CompleteAsync();
        
        await domainEventPublisher.PublishAsync(new ContributionUpdatedEvent(contribution.Id));

        
        return contribution;
    }

    public async Task<bool> Handle(DeleteContributionCommand command)
    {
        var contribution = await contributionRepository.FindByStringIdAsync(command.Id);
        
        if(contribution == null) throw new Exception("Contribution not found");

        contributionRepository.Remove(contribution);
        await unitOfWork.CompleteAsync();

        return true;
    }
    
    
    
}
