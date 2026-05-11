using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.Commands;

namespace com.split.backend.IAM.Domain.Services;

public interface IUserIncomeCommandService
{
    Task<UserIncome?> Handle(CreateUserIncomeCommand command);
    Task<UserIncome?> Handle(UpdateUserIncomeCommand command);
    
}