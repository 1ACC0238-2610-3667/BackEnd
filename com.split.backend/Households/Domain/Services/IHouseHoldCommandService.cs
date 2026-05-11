using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Commands;

namespace com.split.backend.Households.Domain.Services;

public interface IHouseHoldCommandService
{
    public Task<HouseHold?> Handle(CreateHouseholdCommand command);

    public Task<bool> Handle(DeleteHouseHoldCommand command);
    
    public Task<HouseHold?> Handle(UpdateHouseHoldCommand command);
}