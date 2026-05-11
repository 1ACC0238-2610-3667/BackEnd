using com.split.backend.MemberContributions.Domain.Model.Aggregates;
using com.split.backend.MemberContributions.Interface.REST.Resources;

namespace com.split.backend.MemberContributions.Interface.REST.Transform;

public static class MemberContributionFromEntityAssembler
{
    public static MemberContributionResource ToResourceFromEntity(MemberContribution entity)
    {
        return new MemberContributionResource(entity.Id, entity.ContributionId, entity.MemberId,
            entity.Amount, entity.Status.ToString(), entity.PayedAt.ToString("MM/dd/yyyy"));
    }
}