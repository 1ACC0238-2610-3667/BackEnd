using com.split.backend.IAM.Domain.Model.Commands;

namespace com.split.backend.IAM.Domain.Model.Aggregates;

public partial class UserIncome
{
    public string Id { get; set; }
    public long UserId { get; set; }
    public decimal Income { get; set; }

    public UserIncome()
    {
        this.Id = System.Guid.NewGuid().ToString();
        this.UserId = 0;
        this.Income = 0m;
        this.CreatedDate = DateTimeOffset.UtcNow;
        this.UpdatedDate = DateTimeOffset.UtcNow;
    }

    public UserIncome(long userId, decimal income)
    {
        this.Id = $"UI-{Guid.NewGuid()}";
        this.UserId = userId;
        this.Income = income;
        this.CreatedDate = DateTimeOffset.UtcNow;
        this.UpdatedDate = DateTimeOffset.UtcNow;
    }

    public UserIncome(CreateUserIncomeCommand command)
    {
        this.Id = $"UI-{Guid.NewGuid()}";
        this.UserId = command.UserId;
        this.Income = command.Income;
        this.CreatedDate= DateTimeOffset.UtcNow;
        this.UpdatedDate = DateTimeOffset.UtcNow;
    }

    public UserIncome Update(UpdateUserIncomeCommand command)
    {
        this.Income = command.Income;
        this.UpdatedDate = DateTimeOffset.UtcNow;

        return this;
    }
}
