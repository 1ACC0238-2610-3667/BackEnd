using com.split.backend.Contributions.Domain.Model.Commands;
using com.split.backend.Contributions.Interface.REST.Resources;

namespace com.split.backend.Contributions.Interface.REST.Transform;

public static class UpdateContributionCommandFromResourceAssembler
{
    public static UpdateContributionCommand ToCommandFromResource(string id, UpdateContributionResource resource)
    {
        if(resource is null) throw new ArgumentNullException(nameof(resource));

        return new UpdateContributionCommand(
            id,
            resource.Description,
            resource.DeadlineForMembers?.ToString(),
            (int?)resource.Strategy);
    } 
}