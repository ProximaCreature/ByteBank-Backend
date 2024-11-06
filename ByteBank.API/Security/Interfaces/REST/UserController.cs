using ByteBank.API.Security.Application.Features;
using ByteBank.API.Security.Domain.Models.Commands;
using ByteBank.API.Security.Domain.Models.Queries;
using ByteBank.API.Security.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBank.API.Security.Interfaces.REST;

[ApiController]
[Route("/api/v1/users")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserCommandService _userCommandService;
    private readonly IUserQueryService _userQueryService;

    public UserController(IUserCommandService userCommandService, IUserQueryService userQueryService)
    {
        _userCommandService = userCommandService;
        _userQueryService = userQueryService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetUser([FromRoute] int id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _userQueryService.Handle(query);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _userCommandService.Handle(command);
        return StatusCode(StatusCodes.Status201Created, result);
    }
    
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await _userCommandService.Handle(command);
        return Ok(result);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserCommand command)
    {
        var results = await _userCommandService.Handle(id, command);
        return Ok(results);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var command = new DeleteUserCommand(id);
        var result = await _userCommandService.Handle(command);
        return Ok("User deleted");
    }
    
}