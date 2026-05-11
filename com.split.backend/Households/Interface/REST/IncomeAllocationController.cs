using System.Net.Mime;
using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Domain.Models.Queries;
using com.split.backend.Households.Domain.Services;
using com.split.backend.Households.Interface.REST.Resources;
using com.split.backend.Households.Interface.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace com.split.backend.Households.Interface.REST;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class IncomeAllocationController(
    IIncomeAllocationCommandService commandService,
    IIncomeAllocationQueryService queryService) : ControllerBase
{
    [HttpGet("byHousehold/{householdId}")]
    [SwaggerOperation("Get Income Allocations by Household", "Get Income Allocations by Household Id")]
    [SwaggerResponse(200, "The IncomeAllocations were found and returned", typeof(IncomeAllocationResource))]
    [SwaggerResponse(404, "The IncomeAllocations were not found")]
    public async Task<IActionResult> GetIncomeAllocationByHouseholdId(string householdId)
    {
        var getIncomeAllocationsByHouseHoldIdQuery = new GetIncomeAllocationByHouseHoldIdQuery(householdId);
        var incomeAllocations = await queryService.Handle(getIncomeAllocationsByHouseHoldIdQuery);
        if(incomeAllocations == null) return NoContent();
        var incomeAllocationsResource =
            incomeAllocations.Select(IncomeAllocationResourceFromEntityAssembler.ToResourceFromEntity);
        
        return Ok(incomeAllocationsResource);
    }
    
    [HttpGet("byUserId/{userId:long}")]
    [SwaggerOperation("Get Income Allocations by UserId", "Get Income Allocations by User Id")]
    [SwaggerResponse(200, "The IncomeAllocations were found and returned", typeof(IncomeAllocationResource))]
    [SwaggerResponse(404, "The IncomeAllocations were not found")]
    public async Task<IActionResult> GetIncomeAllocationByUserId(long userId)
    {
        var getIncomeAllocationsByUserIdQuery = new GetIncomeAllocationByUserIdQuery(userId);
        var incomeAllocations = await queryService.Handle(getIncomeAllocationsByUserIdQuery);
        if(incomeAllocations == null || !incomeAllocations.Any()) return NoContent();
        var incomeAllocationsResource =
            incomeAllocations.Select(IncomeAllocationResourceFromEntityAssembler.ToResourceFromEntity);
        
        return Ok(incomeAllocationsResource);
    }

    [HttpPost]
    [SwaggerOperation("Create IncomeAllocation", "Create a new income allocation")]
    [SwaggerResponse(201, "The IncomeAllocation was created", typeof(IncomeAllocationResource))]
    [SwaggerResponse(400, "The IncomeAllocation was not created")]
    public async Task<IActionResult> CreateIncomeAllocation([FromBody] CreateIncomeAllocationResource resource)
    {
        var createIncomeAllocationCommand =  CreateIncomeAllocationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var incomeAllocation = await commandService.Handle(createIncomeAllocationCommand);
        if(incomeAllocation == null) return BadRequest();
        var incomeAllocationResource = IncomeAllocationResourceFromEntityAssembler.ToResourceFromEntity(incomeAllocation);
        return CreatedAtAction(nameof(GetIncomeAllocationByHouseholdId), new { householdId = incomeAllocation.HouseholdId}, incomeAllocationResource );
    }

    
    
    [HttpPut("byId/{id}")]
    [SwaggerOperation("Update IncomeAllocation", "UpdateIncomeAllocation")]
    [SwaggerResponse(200, "The IncomeAllocation was successfully updated", typeof(IncomeAllocationResource))]
    [SwaggerResponse(400, "The IncomeAllocation was not updated/invalid")]
    public async Task<IActionResult> UpdateIncomeAllocation(string id,
        [FromBody] UpdateIncomeAllocationResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var updateIncomeAllocationCommand =
                UpdateIncomeAllocationCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var incomeAllocation = await commandService.Handle(updateIncomeAllocationCommand);
            if (incomeAllocation is null) return NotFound();
            var incomeAllocationResource =
                IncomeAllocationResourceFromEntityAssembler.ToResourceFromEntity(incomeAllocation);
            return Ok(incomeAllocationResource);
        }
        catch (Exception e)
        {

            return BadRequest(new { message = e.Message });
        }
    }
    
    

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete Income Allocation", OperationId = "DeleteIncomeAllocation")]
    [SwaggerResponse(200, "The IncomeAllocation was successfully deleted", typeof(IncomeAllocationResource))]
    [SwaggerResponse(404, "The IncomeAllocation was invalid")]
    public async Task<IActionResult> DeleteIncomeALlocationById([FromRoute] string id)
    {
        var deleteCommand = new DeleteIncomeAllocationCommand(id);
        var result = await commandService.Handle(deleteCommand);
        if (!result) return NotFound();
        return Ok(new { message = "The IncomeAllocation was successfully deleted" });
    }
}
