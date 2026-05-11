using com.split.backend.Households.Domain.Models.Commands;

namespace com.split.backend.Households.Domain.Models.Aggregates;

public partial class IncomeAllocation
{
    public string Id { get; set; }
    public long UserId { get; set; }
    public string HouseholdId { get; set; }
    public Decimal Percentage { get; set; }


    public IncomeAllocation()
    {
        this.Id = $"IA-{Guid.NewGuid()}";
        this.UserId = -1;
        this.HouseholdId = string.Empty;
        this.Percentage = 0;
        this.CreatedDate = DateTimeOffset.UtcNow;
        this.UpdatedDate = DateTimeOffset.UtcNow;
    }

    public IncomeAllocation(long userId, string householdId, Decimal percentage)
    {
        this.Id = $"IA-{Guid.NewGuid()}";
        this.UserId = userId;
        this.HouseholdId = householdId;
        this.Percentage = percentage;
        this.CreatedDate = DateTimeOffset.UtcNow;
        this.UpdatedDate = DateTimeOffset.UtcNow;
    }

    public IncomeAllocation(CreateIncomeAllocationCommand command)
    {
        this.Id = $"IA-{Guid.NewGuid()}";
        this.UserId = command.UserId;
        this.HouseholdId = command.HouseholdId;
        this.Percentage = command.Percentage;
        this.CreatedDate = DateTimeOffset.UtcNow;
        this.UpdatedDate = DateTimeOffset.UtcNow ;
    }

    public IncomeAllocation(IncomeAllocationUpsertCommand command)
    {
        this.Id = $"IA-{Guid.NewGuid()}";
        this.UserId = command.UserId;
        this.HouseholdId = command.HouseholdId;
        this.Percentage = command.Percentage;
        this.CreatedDate = DateTimeOffset.UtcNow;
        this.UpdatedDate = DateTimeOffset.UtcNow;
    }

    public IncomeAllocation Update(UpdateIncomeAllocationCommand command)
    {
        if(command.UserId != null) this.UserId = (long)command.UserId;
        if(!string.IsNullOrWhiteSpace(command.HouseholdId)) this.HouseholdId = command.HouseholdId;
        if(command.Percentage != null) this.Percentage = (decimal)command.Percentage;
        this.UpdatedDate = DateTimeOffset.UtcNow;

        return this;
    }
    
}
