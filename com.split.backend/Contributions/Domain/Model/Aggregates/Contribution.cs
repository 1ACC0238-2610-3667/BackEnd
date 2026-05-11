using com.split.backend.Contributions.Domain.Model.Commands;
using com.split.backend.Contributions.Domain.Model.ValueObjects;
using com.split.backend.MemberContributions.Domain.Model.ValueObjects;

namespace com.split.backend.Contributions.Domain.Model.Aggregates;

public partial class Contribution
{
    public string Id { get; set; }
    public string BillId { get; set; }
    public string HouseholdId { get; set; }
    public string Description { get; set; }
    public DateTime? DeadlineForMembers { get; set; }
    public EStrategy? Strategy { get; set; }


    public Contribution()
    {
        this.Id = "CN" + DateTime.Now.ToString("yyyyMMddHHmmss");
        this.BillId = string.Empty;
        this.HouseholdId = string.Empty;
        this.Description = string.Empty;
        this.DeadlineForMembers = null;
        this.Strategy = null;
    }

    public Contribution(string billId, string householdId,
        string description, string? deadlineForMembers, int? strategy)
    {
        this.Id = "CN" + DateTime.Now.ToString("yyyyMMddHHmmss");
        this.BillId = billId;
        this.HouseholdId = householdId;
        this.Description = description ?? string.Empty;
        this.DeadlineForMembers = !string.IsNullOrWhiteSpace(deadlineForMembers)
            ? DateTime.Parse(deadlineForMembers)
            : null;
        this.Strategy = (strategy.HasValue && strategy.Value > 0)
            ? (EStrategy)strategy.Value
            : null;
    }

    public Contribution(CreateContributionCommand command)
    {
        this.Id = "CN" + DateTime.Now.ToString("yyyyMMddHHmmss");
        this.BillId = command.BillId;
        this.HouseholdId = command.HouseholdId;
        this.Description = command.Description ?? string.Empty;
        this.DeadlineForMembers = !string.IsNullOrWhiteSpace(command.DeadlineForMembers)
            ? DateTime.Parse(command.DeadlineForMembers)
            : null;
        this.Strategy = (command.Strategy.HasValue && command.Strategy.Value > 0)
            ? (EStrategy)command.Strategy.Value
            : null;
    }

    public Contribution Update(UpdateContributionCommand command)
    {
        if(!string.IsNullOrWhiteSpace(command.Description)) this.Description = command.Description;
        if (!string.IsNullOrWhiteSpace(command.DeadlineForMember)) this.DeadlineForMembers = DateTime.Parse(command.DeadlineForMember);
        if(command.Strategy != null && command.Strategy >0) this.Strategy = (EStrategy)command.Strategy;

        return this;
    }
    
    
}
