using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Contributions.Domain.Model.Aggregates;
using com.split.backend.Contributions.Domain.Model.Queries;

namespace com.split.backend.Contributions.Domain.Services;

public interface IContributionQueryService
{
    Task<IEnumerable<Contribution?>> Handle(GetAllContributionsQuery query);
    Task<IEnumerable<Contribution?>> Handle(GetContributionsByBillIdQuery query);
    Task<IEnumerable<Contribution?>> Handle(GetContributionsByHouseholdIdQuery query);
    Task<Contribution?> Handle(GetContributionByIdQuery query);
}