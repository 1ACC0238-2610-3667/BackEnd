namespace com.split.backend.IAM.Domain.Model.Commands;

public record SignInCommand(string EmailAddress, string Password);