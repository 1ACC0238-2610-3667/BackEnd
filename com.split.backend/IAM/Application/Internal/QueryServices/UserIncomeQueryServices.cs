using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.Commands;
using com.split.backend.IAM.Domain.Model.Queries;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.IAM.Domain.Services;

namespace com.split.backend.IAM.Application.Internal.QueryServices;

public class UserIncomeQueryServices(IUserIncomeRepository userIncomeRepository) : IUserIncomeQueryService
{
    public async Task<UserIncome?> Handle(GetUserIncomeByIdQuery query)
    {
        return await userIncomeRepository.FindByStringIdAsync(query.Id);
    }

    public async Task<UserIncome?> Handle(GetUserIncomeByUserIdQuery query)
    {
        return await userIncomeRepository.FindByUserIdAsync(query.UserId);
    }
    
}