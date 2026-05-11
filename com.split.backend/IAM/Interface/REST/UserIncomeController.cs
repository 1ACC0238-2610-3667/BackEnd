using System.Net.Mime;
using com.split.backend.IAM.Domain.Model.Queries;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;
using com.split.backend.IAM.Interface.REST.Resources.UserIncome;
using com.split.backend.IAM.Interface.REST.Transform.UserIncome;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace com.split.backend.IAM.Interface.REST;

[ApiController]
[Authorize]
[Route("api/v1/user-income")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag( "Available UserIncome Endpoints")]
public class UserIncomeController(
    IUserIncomeCommandService userIncomeCommandService,
    IUserIncomeQueryService userIncomeQueryService) : ControllerBase
{

    [HttpPost]
    [SwaggerOperation("Create UserIncome", "Create a new UserIncome")]
    [SwaggerResponse(201, "The request was successful")]
    [SwaggerResponse(400, "The request was invalid")]
    public async Task<IActionResult> CreateUserIncome(CreatedUserIncomeResource resource)
    {
        try
        {
            var createUserIncomeCommand = CreateUserIncomeCommandFromResourceAssembler.ToCommandFromResource(resource);
            var userIncome = await userIncomeCommandService.Handle(createUserIncomeCommand);

            if (userIncome == null) return BadRequest();
            
            var userIncomeResource = UserIncomeResourceFromEntityAssembler.ToResourceFromEntity(userIncome);
            return CreatedAtAction(nameof(GetUserIncomeById), new { id = userIncomeResource.Id }, userIncomeResource);

        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation("Get UserIncome by ID", OperationId = "GetUserIncomeById")]
    [SwaggerResponse(200, "The Contributions was found", typeof(UserIncomeResource))]
    public async Task<IActionResult> GetUserIncomeById([FromRoute] string id)
    {
        var getUserIncomeByIdQuery = new GetUserIncomeByIdQuery(id);
        var userIncome = await userIncomeQueryService.Handle(getUserIncomeByIdQuery);
        if (userIncome is null) return NotFound();

        var userIncomeResource = UserIncomeResourceFromEntityAssembler.ToResourceFromEntity(userIncome);
        
        return Ok(userIncomeResource);
    }

    [HttpGet("byUserId/{userId:long}")]
    [SwaggerOperation("Get UserIncome by User Id", OperationId = "GetUserIncomeByUserId")]
    [SwaggerResponse(200, "The UserIncome was found", typeof(UserIncomeResource))]
    public async Task<IActionResult> GetUserIncomeByUserId([FromRoute] long userId)
    {
        var getUserIncomeByUserIdQuery = new GetUserIncomeByUserIdQuery(userId);
        
        var userIncome = await userIncomeQueryService.Handle(getUserIncomeByUserIdQuery);
        if(userIncome is null) return NoContent();

        var userIncomeResource = UserIncomeResourceFromEntityAssembler.ToResourceFromEntity(userIncome);
        return Ok(userIncomeResource);
    }


   
    [HttpPut("byId/{id}")]
    [SwaggerOperation("Update UserIncome", OperationId = "UpdateUserIncome")]
    [SwaggerResponse(200, "The userIncome was successfully updated", typeof(UserIncomeResource))]
    [SwaggerResponse(404, "The userIncome was not found")]
    [SwaggerResponse(400, "The userIncome was invalid")]
    public async Task<IActionResult> UpdateContributionById([FromRoute] string id, [FromBody] UpdatedUserIncomeResource resource)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var updateUserIncomeCommand = UpdateUserIncomeCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var userIncome = await userIncomeCommandService.Handle(updateUserIncomeCommand);
            if(userIncome is null) return NotFound();
            var userIncomeResource = UserIncomeResourceFromEntityAssembler.ToResourceFromEntity(userIncome);
            return Ok(userIncomeResource);
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
        
    }
    
}
