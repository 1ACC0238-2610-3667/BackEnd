using com.split.backend.IAM.Domain.Model.Commands;
using com.split.backend.IAM.Interface.REST.Resources.UserIncome;

namespace com.split.backend.IAM.Interface.REST.Transform.UserIncome;

public static class UpdateUserIncomeCommandFromResourceAssembler
{
    public static UpdateUserIncomeCommand ToCommandFromResource(string Id, UpdatedUserIncomeResource resource)
    {
        if(resource is null) throw new ArgumentNullException(nameof(resource));

        return new UpdateUserIncomeCommand(
            Id,
            resource.Income);
    }
    
}
