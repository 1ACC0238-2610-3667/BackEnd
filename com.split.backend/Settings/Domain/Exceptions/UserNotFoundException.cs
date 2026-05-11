namespace com.split.backend.Settings.Domain.Exceptions;

public class UserNotFoundException(long userId)
    : Exception($"The user with id '{userId}' was not found in IAM.")
{
}
