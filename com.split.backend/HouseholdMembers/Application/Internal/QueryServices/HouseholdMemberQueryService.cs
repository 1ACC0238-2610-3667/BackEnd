using com.split.backend.HouseholdMembers.Domain.Models.Aggregates;
using com.split.backend.HouseholdMembers.Domain.Models.Queries;
using com.split.backend.HouseholdMembers.Domain.Repositories;
using com.split.backend.HouseholdMembers.Domain.Services;

namespace com.split.backend.HouseholdMembers.Application.Internal.QueryServices;

public class HouseholdMemberQueryService(IHouseholdMemberRepository repository) 
    : IHouseholdMemberQueryService
{
    public async Task<HouseholdMember?> Handle(GetHouseholdMemberByIdQuery query)
    {
        return await repository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<HouseholdMember>> Handle(GetHouseholdMembersByHouseholdIdQuery query)
    {
        return await repository.FindByHouseholdIdAsync(query.HouseholdId);
    }

    public async Task<IEnumerable<HouseholdMember>> Handle(GetHouseholdMembersByUserIdQuery query)
    {
        return await repository.FindByUserIdAsync(query.UserId);
    }

    public async Task<IEnumerable<HouseholdMember>> Handle(GetAllHouseholdMembersQuery query)
    {
        return await repository.ListAsync();
    }
}

