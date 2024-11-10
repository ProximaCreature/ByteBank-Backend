using ByteBank.API.BillDiscount.Domain.Models.Commands;
using ByteBank.API.BillDiscount.Domain.Models.Queries;
using ByteBank.API.BillDiscount.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBank.API.BillDiscount.Interfaces.REST;

[ApiController]
[Route("/api/v1/bills")]
[Produces("application/json")]
public class BillController : ControllerBase
{
    private readonly IBillCommandService _billCommandService;
    private readonly IBillQueryService _billQueryService;

    public BillController(IBillCommandService billCommandService, IBillQueryService billQueryService)
    {
        _billCommandService = billCommandService;
        _billQueryService = billQueryService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBill([FromBody] CreateBillCommand command)
    {
        var result = await _billCommandService.Handle(command);
        return StatusCode(StatusCodes.Status201Created, result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBills()
    {
        var query = new GetAllBillsQuery();
        var result = await _billQueryService.Handle(query);
        return StatusCode(StatusCodes.Status200OK, result);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetBillById([FromRoute] int id)
    {
        var query = new GetBillByIdQuery(id);
        var result = await _billQueryService.Handle(query);
        return Ok(result);
    }
}