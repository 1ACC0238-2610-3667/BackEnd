using com.split.backend.Contributions.Domain.Model.Aggregates;
using com.split.backend.Contributions.Interface.REST.Resources;

namespace com.split.backend.Contributions.Interface.REST.Transform;

public static class ContributionResourceFromEntityAssembler
{
    public static ContributionResource ToResourceFromEntity(Contribution entity)
    {
        return new ContributionResource(
            entity.Id,
            entity.BillId,
            entity.HouseholdId,
            entity.Description,
            entity.DeadlineForMembers?.ToString("O") ?? string.Empty,
            entity.Strategy.HasValue ? (int)entity.Strategy.Value : 0);
    }
}
