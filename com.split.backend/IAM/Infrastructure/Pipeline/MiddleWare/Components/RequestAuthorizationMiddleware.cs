using com.split.backend.IAM.Application.Internal.OutboundServices;
using com.split.backend.IAM.Domain.Model.Queries;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;

namespace com.split.backend.IAM.Infrastructure.Pipeline.MiddleWare.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService
    )
    {
        Console.WriteLine("Entering InvokeAsync");
        var endpoint = context.GetEndpoint();
        var allowAnonymous = endpoint?.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute)) ?? false;
        var path = context.Request.Path.Value?.ToLower();

        if (allowAnonymous ||
            path == "/" ||
            path.Contains("/swagger") ||
            path.Contains("/api-docs") ||
            path.Contains("/health") ||
            path.Contains("/index.html"))
        {
            Console.WriteLine("Skipping authorization - Middleware");
            await next(context);
            return;
        }
        Console.WriteLine("Entering Authorization");
        var token = context.Request.Headers["Authorization"]
            .FirstOrDefault()?.Split(" ").Last();

        if (token == null) throw new Exception("Null or invalid token");

        var userId = await tokenService.ValidateToken(token);
        
        if(userId == null) throw new Exception("Invalid token");

        var getUserByIdQuery = new GetUsersByIdQuery(userId.Value);
        var user = await userQueryService.Handle(getUserByIdQuery);
        Console.WriteLine("Successfull authorization. Updating Context ...");
        context.Items["User"] = user;
        Console.WriteLine("Continuing with Middleware Pipeline");
        
        await next(context);
    }
    
}