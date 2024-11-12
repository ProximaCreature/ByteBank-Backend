using ByteBank.API.Wallet.Domain.Models.Commands;
using ByteBank.API.Wallet.Domain.Models.Queries;
using ByteBank.API.Wallet.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace ByteBank.API.Wallet.Interface.REST;

[ApiController]
[Route("api/v1/wallets")]
[Produces("application/json")]
public class WalletController : ControllerBase
{
    private readonly IWalletCommandService _walletCommandService;
    private readonly IWalletQueryService _walletQueryService;

    public WalletController(IWalletCommandService walletCommandService, IWalletQueryService walletQueryService)
    {
        _walletCommandService = walletCommandService;
        _walletQueryService = walletQueryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletCommand command)
    {
        var result = await _walletCommandService.Handle(command);
        return StatusCode(StatusCodes.Status201Created, result);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetWalletById([FromRoute] int id)
    {
        var query = new GetWalletByIdQuery(id);
        var result = await _walletQueryService.Handle(query);
        return Ok(result);
    }
}