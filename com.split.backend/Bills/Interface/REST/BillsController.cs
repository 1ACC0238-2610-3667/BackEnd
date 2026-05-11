using System.Net.Mime;
using com.split.backend.Bills.Domain.Models.Commands;
using com.split.backend.Bills.Domain.Models.Queries;
using com.split.backend.Bills.Domain.Services;
using com.split.backend.Bills.Interface.REST.Resources;
using com.split.backend.Bills.Interface.REST.Transform;
using com.split.backend.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace com.split.backend.Bills.Interface.REST;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Bills Endpoints")]
public class BillsController(
    IBillCommandService billCommandService,
    IBillQueryService billQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Bills", OperationId = "GetAllBills")]
    [SwaggerResponse(200, "The bills were found and returned", typeof(BillResource))]
    [SwaggerResponse(404, "The bills were not found")]
    public async Task<IActionResult> GetAllBills()
    {
        var getAllBillsQuery = new GetAllBillsQuery();
        var bills = await billQueryService.Handle(getAllBillsQuery);
        if(bills is null)
            throw new ArgumentNullException(nameof(bills));
        
        var billResources = bills.Select(BillResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(billResources);
    }

    [HttpGet("byHousehold/{householdId}")]
    [SwaggerOperation("Get By Household", OperationId = "GetByHousehold")]
    [SwaggerResponse(200, "The bills were found and returned", typeof(IEnumerable<BillResource>))]
    [SwaggerResponse(404, "The bills were not found")]
    public async Task<IActionResult> GetBillsByHousehold(string householdId)
    {
        var getBillsByHouseHoldIdQuery = new GetBillsByHouseholdIdQuery(householdId);
        var bills = await billQueryService.Handle(getBillsByHouseHoldIdQuery);
        if(bills is null)
            throw new ArgumentNullException(nameof(bills));
        
        var billResources = bills.Select(BillResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(billResources);
    }

    [HttpPost]
    [SwaggerOperation("Create Bill", OperationId = "CreateBill")]
    [SwaggerResponse(201, "The bill was created", typeof(BillResource))]
    [SwaggerResponse(400, "The bill was invalid")]
    public async Task<IActionResult> CreateBill([FromBody] CreateBillResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var command = CreateBillCommandFromResourceAssembler.ToCommandFromResource(resource);
            var bill = await billCommandService.Handle(command);
            if (bill is null) return BadRequest(new { message = "Invalid data" });
            var billResource = BillResourceFromEntityAssembler.ToResourceFromEntity(bill);
            return CreatedAtAction(nameof(GetBillsByHousehold), new { householdId = bill.HouseholdId }, billResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpPut("byId/{id}")]
    [SwaggerOperation("Update Bill", OperationId = "UpdateBill")]
    [SwaggerResponse(200, "The bill was successfully updated", typeof(BillResource))]
    [SwaggerResponse(404, "The bill were not found")]
    [SwaggerResponse(400, "The bill was invalid")]
    public async Task<IActionResult> UpdateBillById([FromRoute] string id, [FromBody] UpdateBillResource resource)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
           var updateBillCommand = UpdateBillCommandFromResourceAssembler.ToCommandFromResource(id, resource);
           var bill = await billCommandService.Handle(updateBillCommand);
           if(bill is null) return NotFound();
           var billResource = BillResourceFromEntityAssembler.ToResourceFromEntity(bill);
           return Ok(billResource);
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
        
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete Bill", OperationId = "DeleteBill")]
    [SwaggerResponse(200, "The bill was successfully deleted", typeof(BillResource))]
    [SwaggerResponse(404, "The bill was invalid")]
    public async Task<IActionResult> DeleteBillById([FromRoute] string id)
    {
        var deleteCommand = new DeleteBillCommand(id);
        var result = await billCommandService.Handle(deleteCommand);
        if(!result) return NotFound();
        return Ok(new { message = "The bill was successfully deleted" });
    }
}
