using library.Domain.Domain.User.Write.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace library.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginCommand cmd)
    {
        var result  = await _mediator.Send(cmd);
        
        if (!result.Success)
            return Unauthorized(new { error = result.ErrorMessage });
        
        return Ok(new { token = result.Data });
    }
}