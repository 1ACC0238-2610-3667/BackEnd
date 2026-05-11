namespace com.split.backend.IAM.Interface.REST.Resources;

public record UpdateUserResource(string EmailAddress, string PersonName, string Password);