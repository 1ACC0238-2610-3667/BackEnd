using System.Net.Mime;
using com.split.backend.MemberContributions.Domain.Model.Commands;
using com.split.backend.MemberContributions.Domain.Model.Queries;
using com.split.backend.MemberContributions.Domain.Services;
using com.split.backend.MemberContributions.Interface.REST.Resources;
using com.split.backend.MemberContributions.Interface.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace com.split.backend.MemberContributions.Interface.REST;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available MemberContributions Endpoints")]
public class MemberContributionController(
    IMemberContributionCommandService memberContributionCommandService,
    IMemberContributionQueryService memberContributionQueryService) 
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All MemberContributions", OperationId = "GetAllMemberContributions")]
    [SwaggerResponse(200, "The Contributions were found", typeof(MemberContributionResource))]
    public async Task<IActionResult> GetAllMemberContributions()
    {
        var getAllContributionsQuery = new GetAllMemberContributionsQuery();
        var contributions = await memberContributionQueryService.Handle(getAllContributionsQuery);
        if (contributions is null)
            throw new ArgumentNullException(nameof(contributions));

        var contributionsResource = contributions.Select(MemberContributionFromEntityAssembler.ToResourceFromEntity);
        return Ok(contributionsResource);
    }

    [HttpGet("byContributionId/{contributionId}")]
    [SwaggerOperation("Get MemberContribution by Contribution Id", OperationId = "GetMemberContributionByContributionId")]
    [SwaggerResponse(200, "The MemberContribution was found", typeof(MemberContributionResource))]
    public async Task<IActionResult> GetMemberContributionByContributionId([FromRoute] string contributionId)
    {
        var getContributionByBillIdQuery = new GetMemberContributionsByContributionIdQuery(contributionId);
        
        var contributions = await memberContributionQueryService.Handle(getContributionByBillIdQuery);
        if(contributions is null) return NotFound();

        var contributionResources = contributions.Select(MemberContributionFromEntityAssembler.ToResourceFromEntity);
        return Ok(contributionResources);
    }

    [HttpGet("byMemberId/{memberId}")]
    [SwaggerOperation("Get MemberContribution By MemberId", OperationId = "GetMemberContributionsByMemberId")]
    [SwaggerResponse(200, "The MemberContribution was found", typeof(MemberContributionResource))]
    public async Task<IActionResult> GetMemberContributionByMemberId([FromRoute] string memberId)
    {
        var getContributionsByHouseholdIdQuery = new GetMemberContributionsByMemberIdQuery(memberId);
        var contributions = await memberContributionQueryService.Handle(getContributionsByHouseholdIdQuery);
        if (contributions is null) return NotFound();
        
        var contributionResources = contributions.Select(MemberContributionFromEntityAssembler.ToResourceFromEntity);
        return Ok(contributionResources);
        
    }

    [HttpPost]
    [SwaggerOperation("Create MemberContribution", OperationId = "CreateMemberContribution")]
    [SwaggerResponse(201, "The MemberContribution was created", typeof(MemberContributionResource))]
    [SwaggerResponse(400, "The MemberContribution was invalid")]
    public async Task<IActionResult> CreateMemberContribution([FromBody] CreateMemberContributionResource resource)
    {
        if (resource is null) return BadRequest();

        var command = CreateMemberContributionFromResourceAssembler.ToCommandFromResource(resource);
        var contribution = await memberContributionCommandService.Handle(command);
        if (contribution is null) return BadRequest();

        var contributionResource = MemberContributionFromEntityAssembler.ToResourceFromEntity(contribution);
        return CreatedAtAction(nameof(GetMemberContributionByContributionId), new { contributionId = contributionResource.ContributionId }, contributionResource);
    }
   
    

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete MemberContribution", OperationId = "DeleteMemberContribution")]
    [SwaggerResponse(200, "The MemberContribution was successfully deleted", typeof(MemberContributionResource))]
    [SwaggerResponse(404, "The MemberContribution was invalid")]
    public async Task<IActionResult> DeleteMemberContributionById([FromRoute] string id)
    {
        var deleteCommand = new DeleteMemberContributionCommand(id);
        var result = await memberContributionCommandService.Handle(deleteCommand);
        if(!result) return NotFound();
        return Ok(new { message = "The member contribution was successfully deleted" });
    } 
    
    
}
