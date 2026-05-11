using System.Net.Mime;
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
public class HouseHoldController(
    IHouseHoldCommandService commandService,
    IHouseHoldQueryService queryService) : ControllerBase
{
    [HttpGet("{id}")]
    [SwaggerOperation("Get HouseHold ById", "Get a Household by its unique identifier",
        OperationId = "GetHouseHoldById")]
    [SwaggerResponse(200, "The household was found and returned", typeof(HouseHoldResource))]
    [SwaggerResponse(404, "The household was not found")]
    public async Task<IActionResult> GetHouseHoldById(string id)
    {
        var getHouseholdByIdQuery = new GetHouseHoldByIdQuery(id);
        var houseHold = await queryService.Handle(getHouseholdByIdQuery);
        if (houseHold is null) return NotFound();
        var householdResource = HouseholdResourceFromEntityAssembler.ToResourceFromEntity(houseHold);
        return Ok(householdResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Household", "Creates a new Household")]
    [SwaggerResponse(201, "The HouseHold has been created")]
    [SwaggerResponse(400, "The household was not created")]
    public async Task<IActionResult> CreateHousehold([FromBody] CreateHouseHoldResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var createHouseHoldCommand = CreateHouseHoldCommandFromResourceAssembler.ToCommandFromResource(resource);
            var household = await commandService.Handle(createHouseHoldCommand);
            if (household is null) return BadRequest(new { message = "RepresentativeId not found or invalid data." });
            var houseHoldResource = HouseholdResourceFromEntityAssembler.ToResourceFromEntity(household);
            return CreatedAtAction(nameof(GetHouseHoldById), new { id = household.Id }, houseHoldResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Update Household", "Updates an existing Household")]
    [SwaggerResponse(200, "The HouseHold has been updated", typeof(HouseHoldResource))]
    [SwaggerResponse(404, "The household was not found")]
    public async Task<IActionResult> UpdateHousehold(string id, [FromBody] UpdateHouseHoldResource resource)
    {
        Console.WriteLine($"PUT Household INVOCADO: id={id}");
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var updateCommand = UpdateHouseHoldCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var household = await commandService.Handle(updateCommand);
            if (household is null) return NotFound();
            var houseHoldResource = HouseholdResourceFromEntityAssembler.ToResourceFromEntity(household);
            return Ok(houseHoldResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("representative/{representativeId}")]
    [SwaggerOperation("Get HouseHolds By Representative", "Get all HouseHolds for a representative",
        OperationId = "GetHouseHoldsByRepresentativeId")]
    [SwaggerResponse(200, "The households were found and returned", typeof(IEnumerable<HouseHoldResource>))]
    public async Task<IActionResult> GetHouseholdsByRepresentative(long representativeId)
    {
        Console.WriteLine($"GET /house_hold/representative/{representativeId}");

        var households = await queryService.GetHouseHoldsByRepresentativeId(representativeId);

        if (households == null || !households.Any())
            return Ok(new List<HouseHoldResource>());

        var resources = households
            .Select(HouseholdResourceFromEntityAssembler.ToResourceFromEntity)
            .ToList();

        return Ok(resources);
    }
    
    
}
