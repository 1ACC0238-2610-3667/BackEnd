namespace com.split.backend.IAM.Interface.REST.Resources;

public record SignUpResource(string Email, string Password, string Name, string Role, int Plan, string? HouseholdId);
