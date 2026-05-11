using com.split.backend.IAM.Domain.Model.Commands;
using com.split.backend.IAM.Interface.REST.Resources;

namespace com.split.backend.IAM.Interface.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        var plan = resource.Plan <= 0 ? 1 : resource.Plan; // default Free = 1
        var householdId = string.IsNullOrWhiteSpace(resource.HouseholdId) ? null : resource.HouseholdId.Trim();
        return new SignUpCommand(resource.Email.ToString(), resource.Password, resource.Name, resource.Role, plan, householdId);
    }
}
