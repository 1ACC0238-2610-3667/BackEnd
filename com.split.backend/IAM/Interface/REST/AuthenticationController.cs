using System.Net.Mime;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;
using com.split.backend.IAM.Interface.REST.Resources;
using com.split.backend.IAM.Interface.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace com.split.backend.IAM.Interface.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        var resource = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(
            authenticatedUser.user, 
            authenticatedUser.token,
            authenticatedUser.wasNewUser);
        return Ok(resource);
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource)
    {
        try
        {
            var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
            await userCommandService.Handle(signUpCommand);
            return Ok(new { message = "Signed up successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
}
