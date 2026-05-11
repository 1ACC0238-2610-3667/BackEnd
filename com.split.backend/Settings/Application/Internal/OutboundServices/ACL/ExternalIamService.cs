using com.split.backend.IAM.Domain.Model.Queries;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.Settings.Domain.Exceptions;

namespace com.split.backend.Settings.Application.Internal.OutboundServices.ACL;

public class ExternalIamService(IUserQueryService userQueryService) : IExternalIamService
{
    public async Task EnsureUserExists(long userId)
    {
        if (userId <= 0 || userId > int.MaxValue) throw new UserNotFoundException(userId);

        var user = await userQueryService.Handle(new GetUsersByIdQuery((int)userId));
        if (user is null) throw new UserNotFoundException(userId);
    }
}
