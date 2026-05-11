using System.Net.Mime;
using com.split.backend.Invitations.Domain.Services;
using com.split.backend.Invitations.Interface.REST.Resources;
using com.split.backend.Invitations.Interface.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace com.split.backend.Invitations.Interface.REST;

[ApiController]
[Authorize]
[Route("api/v1/invitations")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Invitation endpoints")]
public class InvitationController(
    IInvitationCommandService commandService,
    IInvitationQueryService queryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Invitation")]
    public async Task<IActionResult> CreateInvitation([FromBody] CreateInvitationResource resource)
    {
        try
        {
            var command = CreateInvitationCommandFromResourceAssembler.ToCommandFromResource(resource);
            var invitation = await commandService.Handle(command);
            var res = InvitationResourceFromEntityAssembler.ToResourceFromEntity(invitation);
            return CreatedAtAction(nameof(GetPending), new { email = res.Email, householdId = res.HouseholdId }, res);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("pending")]
    [AllowAnonymous]
    [SwaggerOperation("Get Pending Invitation by email+household")]
    public async Task<IActionResult> GetPending([FromQuery] string email, [FromQuery] string householdId)
    {
        var invitation = await queryService.FindPendingAsync(email, householdId);
        if (invitation == null) return NotFound();
        var res = InvitationResourceFromEntityAssembler.ToResourceFromEntity(invitation);
        return Ok(res);
    }
}
