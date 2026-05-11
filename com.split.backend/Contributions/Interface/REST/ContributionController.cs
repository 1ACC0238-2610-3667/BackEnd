using System.Net.Mime;
using com.split.backend.Contributions.Domain.Model.Aggregates;
using com.split.backend.Contributions.Domain.Model.Commands;
using com.split.backend.Contributions.Domain.Model.Queries;
using com.split.backend.Contributions.Domain.Services;
using com.split.backend.Contributions.Interface.REST.Resources;
using com.split.backend.Contributions.Interface.REST.Transform;
using com.split.backend.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace com.split.backend.Contributions.Interface.REST;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Contributions Endpoints")]
public class ContributionController(
    IContributionCommandService contributionCommandService,
    IContributionQueryService contributionQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Contributions", OperationId = "GetAllContributions")]
    [SwaggerResponse(200, "The Contributions were found", typeof(ContributionResource))]
    public async Task<IActionResult> GetAllContributions()
    {
        var getAllContributionsQuery = new GetAllContributionsQuery();
        var contributions = await contributionQueryService.Handle(getAllContributionsQuery);
        if (contributions is null)
            throw new ArgumentNullException(nameof(contributions));

        var contributionsResource = contributions.Select(ContributionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(contributionsResource);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get Contribution by ID", OperationId = "GetContributionById")]
    [SwaggerResponse(200, "The Contributions was found", typeof(ContributionResource))]
    public async Task<IActionResult> GetContributionById([FromRoute] string id)
    {
        var getContributionByIdQuery = new GetContributionByIdQuery(id);
        var contribution = await contributionQueryService.Handle(getContributionByIdQuery);
        if (contribution is null) return NotFound();

        var contributionResource = ContributionResourceFromEntityAssembler.ToResourceFromEntity(contribution);
        
        return Ok(contributionResource);
    }

    [HttpGet("byBillId/{billId}")]
    [SwaggerOperation("Get Contribution by Bill Id", OperationId = "GetContributionByBillId")]
    [SwaggerResponse(200, "The Contributions was found", typeof(ContributionResource))]
    public async Task<IActionResult> GetContributionsByBillId([FromRoute] string billId)
    {
        var getContributionByBillIdQuery = new GetContributionsByBillIdQuery(billId);
        
        var contributions = await contributionQueryService.Handle(getContributionByBillIdQuery);
        if(contributions is null) return NotFound();

        var contributionResources = contributions.Select(ContributionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(contributionResources);
    }

    [HttpGet("byHouseHoldId/{householdId}")]
    [SwaggerOperation("Get Contributions By HouseHoldId", OperationId = "GetContributionsByHouseHoldId")]
    [SwaggerResponse(200, "The Contributions was found", typeof(ContributionResource))]
    public async Task<IActionResult> GetContributionsByHouseHoldId([FromRoute] string householdId)
    {
        var getContributionsByHouseholdIdQuery = new GetContributionsByHouseholdIdQuery(householdId);
        var contributions = await contributionQueryService.Handle(getContributionsByHouseholdIdQuery);
        if (contributions is null) return NotFound();
        
        var contributionResources = contributions.Select(ContributionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(contributionResources);
        
    }
    
    [HttpPost]
    [SwaggerOperation("Create Contribution", "Create a new contribution")]
    [SwaggerResponse(201, "The Contribution was created", typeof(ContributionResource))]
    [SwaggerResponse(400, "The Contribution was not created")]
    public async Task<IActionResult> CreateContribution([FromBody] CreateContributionResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createContributionCommand =  CreateContributionCommandFromResourceAssembler.ToCommandFromResource(resource);
        try
        {
            var contribution = await contributionCommandService.Handle(createContributionCommand);
            if(contribution == null) return BadRequest();
            var contributionResource = ContributionResourceFromEntityAssembler.ToResourceFromEntity(contribution);
            return CreatedAtAction(nameof(GetContributionById), new { id = contribution.Id}, contributionResource );
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }


   
    [HttpPut("byId/{id}")]
    [SwaggerOperation("Update Contribution", OperationId = "UpdateContribution")]
    [SwaggerResponse(200, "The contribution was successfully updated", typeof(ContributionResource))]
    [SwaggerResponse(404, "The contribution was not found")]
    [SwaggerResponse(400, "The contribution was invalid")]
    public async Task<IActionResult> UpdateContributionById([FromRoute] string id, [FromBody] UpdateContributionResource resource)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var updateBillCommand = UpdateContributionCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var bill = await contributionCommandService.Handle(updateBillCommand);
            if(bill is null) return NotFound();
            var billResource = ContributionResourceFromEntityAssembler.ToResourceFromEntity(bill);
            return Ok(billResource);
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
        
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete Contribution", OperationId = "DeleteContribution")]
    [SwaggerResponse(200, "The bill was successfully deleted", typeof(ContributionResource))]
    [SwaggerResponse(404, "The bill was invalid")]
    public async Task<IActionResult> DeleteBillById([FromRoute] string id)
    {
        var deleteCommand = new DeleteContributionCommand(id);
        var result = await contributionCommandService.Handle(deleteCommand);
        if(!result) return NotFound();
        return Ok(new { message = "The bill was successfully deleted" });
    }
    
    
    
}
