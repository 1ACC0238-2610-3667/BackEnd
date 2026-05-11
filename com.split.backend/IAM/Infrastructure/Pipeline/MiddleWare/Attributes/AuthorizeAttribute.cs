using com.split.backend.IAM.Domain.Model.Aggregates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace com.split.backend.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous)
        {
            Console.WriteLine("Skipping Authorization Filter");
            return;
        }
        
        var user = (User?)context.HttpContext.Items["User"];

        if (user == null) context.Result = new UnauthorizedResult();

    }
}