using System.Net.Mime;
using com.split.backend.Settings.Application.Internal.CommandServices;
using com.split.backend.Settings.Application.Internal.QueryServices;
using com.split.backend.Settings.Domain.Exceptions;
using com.split.backend.Settings.Domain.Models.Queries;
using com.split.backend.Settings.Domain.Services;
using com.split.backend.Settings.Interface.REST.Resources;
using com.split.backend.Settings.Interface.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace com.split.backend.Settings.Interface.REST;

[ApiController]
[Authorize]
[Route("api/v1/[Controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class SettingsController(
    ISettingsCommandService commandService,
    ISettingsQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get setting by user",
        Description = "Returns the settings configuration for a given user id",
        OperationId = "GetSettingByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Settings found", typeof(SettingResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Settings not found")]
    public async Task<IActionResult> Get([FromQuery] long userId)
    {
        if (userId <= 0) return BadRequest("userId query parameter is required");

        var query = new GetSettingByUserIdQuery(userId);
        var setting = await queryService.Handle(query);
        if (setting is null) return NotFound();

        var resource = SettingResourceFromEntityAssembler.ToResourceFromEntity(setting);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create settings",
        Description = "Creates the settings for a user or returns the existing configuration")]
    [SwaggerResponse(StatusCodes.Status201Created, "Settings created", typeof(SettingResource))]
    [SwaggerResponse(StatusCodes.Status200OK, "Settings already existed", typeof(SettingResource))]
    public async Task<IActionResult> Post([FromBody] CreateSettingResource resource)
    {
        try
        {
            var existing = await queryService.Handle(new GetSettingByUserIdQuery(resource.UserId));
            if (existing is not null)
            {
                return Ok(SettingResourceFromEntityAssembler.ToResourceFromEntity(existing));
            }

            var command = CreateSettingCommandFromResourceAssembler.ToCommandFromResource(resource);
            var setting = await commandService.Handle(command);
            if (setting is null) return BadRequest("Unable to create settings");

            var dto = SettingResourceFromEntityAssembler.ToResourceFromEntity(setting);
            return CreatedAtAction(nameof(Get), new { userId = setting.UserId }, dto);
        }
        catch (UserNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:long}")]
    [SwaggerOperation(
        Summary = "Update settings",
        Description = "Updates the settings configuration identified by id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Settings updated", typeof(SettingResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Settings not found")]
    public async Task<IActionResult> Put(long id, [FromBody] UpdateSettingResource resource)
    {
        try
        {
            var command = UpdateSettingCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var updated = await commandService.Handle(command);
            if (updated is null) return NotFound();

            var dto = SettingResourceFromEntityAssembler.ToResourceFromEntity(updated);
            return Ok(dto);
        }
        catch (UserNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }
}
