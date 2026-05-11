namespace com.split.backend.Settings.Application.Internal.OutboundServices.ACL;

public interface IExternalIamService
{
    Task EnsureUserExists(long userId);
}
