namespace com.split.backend.IAM.Domain.Model.Commands;

public record SignUpCommand(string EmailAddress, string Password, 
    string Name, string Role, int Plan, string? HouseholdId);
