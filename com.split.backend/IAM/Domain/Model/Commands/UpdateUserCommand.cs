namespace com.split.backend.IAM.Domain.Model.Commands;

public record UpdateUserCommand(string EmailAddress, string Username, string Password);