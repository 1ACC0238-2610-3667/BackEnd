namespace com.split.backend.Contributions.Domain.Model.Commands;

public record UpdateContributionCommand(string Id,string? Description, 
    string? DeadlineForMember, int? Strategy);