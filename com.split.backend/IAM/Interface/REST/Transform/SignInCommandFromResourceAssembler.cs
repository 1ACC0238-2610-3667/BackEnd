using com.split.backend.IAM.Domain.Model.Commands;
using com.split.backend.IAM.Interface.REST.Resources;

namespace com.split.backend.IAM.Interface.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email.ToString(), resource.Password);
    }
    
}