namespace com.split.backend.IAM.Domain.Model.Queries;

public record GetAllUsersQuery(string? HouseholdId = null, string? Role = null, string? Email = null);