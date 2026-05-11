using System.Net.Mime;
using System.Security.Claims;
using System.Security.Permissions;
using com.split.backend.IAM.Application.Internal.CommandServices;
using com.split.backend.IAM.Domain.Model.Commands;
using com.split.backend.IAM.Domain.Model.Queries;
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
[SwaggerTag("Available User endpoints")]
public class UserController(IUserQueryService userQueryService, IUserCommandService userCommandService) : ControllerBase
{
    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUsersByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user!);
        return Ok(userResource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUserQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUserQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }

    [HttpGet("houseHoldId/{mainHouseHoldId}")]
    public async Task<IActionResult> GetUserByHouseHoldId(string houseHoldId)
    {
        var getUserByMainHouseHoldIdQuery = new GetUserByMainHouseHoldId(houseHoldId);
        var user = await userQueryService.Handle(getUserByMainHouseHoldIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user!);
        return Ok(userResource);
    }

    [HttpPut("byEmail/{emailAddress}")]
    public async Task<IActionResult> UpdateUserByEmail(string emailAddress, [FromBody] UpdateUserResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var command = new UpdateUserCommand(
            emailAddress,
            resource.PersonName,
            resource.Password
        );

        var updatedUser = await userCommandService.Handle(command);
        if (updatedUser == null) return NotFound();

        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(updatedUser);
        return Ok(userResource);
    }



    [HttpDelete("byEmail/{email}")]
    public async Task<IActionResult> DeleteUser(string email)
    {
        var command = new DeleteUserCommand(email);
        var result = await userCommandService.Handle(command);

        if (!result) return NotFound();

        return NoContent();

    }
}
