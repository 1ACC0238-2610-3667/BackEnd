using com.split.backend.MemberContributions.Domain.Model.Aggregates;
using com.split.backend.MemberContributions.Domain.Model.Commands;
using com.split.backend.MemberContributions.Interface.REST.Resources;

namespace com.split.backend.MemberContributions.Interface.REST.Transform;

public static class CreateMemberContributionFromResourceAssembler
{
    public static CreateMemberContributionCommand ToCommandFromResource(CreateMemberContributionResource resource)
    {
        if (resource is null) throw new ArgumentNullException(nameof(resource));
        return new CreateMemberContributionCommand(resource.ContributionId, resource.MemberId, resource.Amount);
    }
    
}
