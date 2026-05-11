using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.Queries;

namespace com.split.backend.IAM.Domain.Services;

public interface IUserIncomeQueryService
{
    Task<UserIncome?> Handle(GetUserIncomeByIdQuery query);
    
    Task<UserIncome?> Handle(GetUserIncomeByUserIdQuery query);
}