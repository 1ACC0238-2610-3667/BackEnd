using com.split.backend.IAM.Domain.Model.Commands;
using com.split.backend.IAM.Interface.REST.Resources.UserIncome;

namespace com.split.backend.IAM.Interface.REST.Transform.UserIncome;

public static class CreateUserIncomeCommandFromResourceAssembler
{
    public static CreateUserIncomeCommand ToCommandFromResource(CreatedUserIncomeResource resource)
    {
        if(resource == null) throw new NullReferenceException(nameof(resource));

        return new CreateUserIncomeCommand(
            resource.UserId,
            resource.Income);
    }
}