using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.Commands;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.IAM.Domain.Services;

public interface IUserCommandService
{
    Task<(User user, string token, bool wasNewUser)> Handle(SignInCommand command);
    
    Task Handle(SignUpCommand command);
    Task <bool>Handle(DeleteUserCommand command);
    Task <User?>Handle(UpdateUserCommand command);
}
