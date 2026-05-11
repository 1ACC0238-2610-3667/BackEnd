using com.split.backend.IAM.Interface.REST.Resources.UserIncome;

namespace com.split.backend.IAM.Interface.REST.Transform.UserIncome;

public static class UserIncomeResourceFromEntityAssembler
{
    public static UserIncomeResource ToResourceFromEntity(Domain.Model.Aggregates.UserIncome entity)
    {
        return new UserIncomeResource(
            entity.Id,
            entity.UserId,
            entity.Income,
            entity.CreatedDate.ToString(),
            entity.UpdatedDate.ToString());
    }
    
}