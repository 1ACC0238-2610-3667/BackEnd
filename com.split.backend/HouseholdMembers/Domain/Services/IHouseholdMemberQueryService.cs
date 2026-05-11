using com.split.backend.HouseholdMembers.Domain.Models.Aggregates;
using com.split.backend.HouseholdMembers.Domain.Models.Queries;

namespace com.split.backend.HouseholdMembers.Domain.Services;

public interface IHouseholdMemberQueryService
{
    Task<HouseholdMember?> Handle(GetHouseholdMemberByIdQuery query);
    
    Task<IEnumerable<HouseholdMember>> Handle(GetHouseholdMembersByHouseholdIdQuery query);
    
    Task<IEnumerable<HouseholdMember>> Handle(GetHouseholdMembersByUserIdQuery query);
    
    Task<IEnumerable<HouseholdMember>> Handle(GetAllHouseholdMembersQuery query);
}

