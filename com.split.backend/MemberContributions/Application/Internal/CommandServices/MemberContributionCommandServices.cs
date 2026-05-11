using com.split.backend.MemberContributions.Domain.Model.Aggregates;
using com.split.backend.MemberContributions.Domain.Model.Commands;
using com.split.backend.MemberContributions.Domain.Model.Events;
using com.split.backend.MemberContributions.Domain.Repositories;
using com.split.backend.MemberContributions.Domain.Services;
using com.split.backend.Shared.Domain.Repositories;
using Cortex.Mediator;

namespace com.split.backend.MemberContributions.Application.Internal.CommandServices;

public class MemberContributionCommandServices(IMemberContributionRepository memberContributionRepository, IUnitOfWork unitOfWork, IMediator domainEventPublisher) : IMemberContributionCommandService
{
    public async Task<MemberContribution?> Handle(CreateMemberContributionCommand command)
    {
        var contribution = new MemberContribution(command);
        
        if(memberContributionRepository.ExistsById(contribution.Id)) throw new Exception("Contribution already exists");
        
        await memberContributionRepository.AddAsync(contribution);
        await unitOfWork.CompleteAsync();


        await domainEventPublisher.PublishAsync(new MemberContributionCreatedEvent(contribution.Id));


        return contribution;
    }


    public async Task<MemberContribution?> Handle(UpdateMemberContributionAmountCommand command)
    {
        var contribution = await memberContributionRepository.FindByStringIdAsync(command.Id);
        if(contribution == null) throw new Exception("Contribution not found");

        contribution.UpdateAmount(command);

        memberContributionRepository.Update(contribution);
        
        await unitOfWork.CompleteAsync();
        
        await domainEventPublisher.PublishAsync(new MemberContributionUpdatedEvent(contribution.Id));

        
        return contribution;
    }

    public async Task<bool> Handle(DeleteMemberContributionCommand command)
    {
        var contribution = await memberContributionRepository.FindByStringIdAsync(command.Id);
        
        if(contribution == null) throw new Exception("Contribution not found");

        memberContributionRepository.Remove(contribution);
        await unitOfWork.CompleteAsync();

        return true;
    } 
}