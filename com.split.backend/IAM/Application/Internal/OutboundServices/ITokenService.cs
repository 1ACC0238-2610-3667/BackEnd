using com.split.backend.IAM.Domain.Model.Aggregates;

namespace com.split.backend.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    
    Task<int?> ValidateToken(string token);
}