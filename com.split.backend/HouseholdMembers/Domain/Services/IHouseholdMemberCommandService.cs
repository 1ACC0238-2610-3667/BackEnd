using com.split.backend.HouseholdMembers.Domain.Models.Aggregates;
using com.split.backend.HouseholdMembers.Domain.Models.Commands;

namespace com.split.backend.HouseholdMembers.Domain.Services;

public interface IHouseholdMemberCommandService
{
    Task<HouseholdMember?> Handle(CreateHouseholdMemberCommand command);
    
    Task<bool> Handle(DeleteHouseholdMemberCommand command);
    
    Task<HouseholdMember?> Handle(UpdateHouseholdMemberCommand command);
    
    Task<HouseholdMember?> Handle(PromoteToRepresentativeCommand command);
    
    Task<HouseholdMember?> Handle(DemoteRepresentativeCommand command);
}

