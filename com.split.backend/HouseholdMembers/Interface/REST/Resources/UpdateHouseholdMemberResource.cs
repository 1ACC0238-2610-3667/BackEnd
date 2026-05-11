namespace com.split.backend.HouseholdMembers.Interface.REST.Resources;

using System.Collections.Generic;

public record UpdateHouseholdMemberResource(
    string? HouseholdId,
    int? UserId,
    bool? IsRepresentative,
    decimal? Income,
    IEnumerable<UpdateIncomeAllocationItemResource>? Allocations
);

