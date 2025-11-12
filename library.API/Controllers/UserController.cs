using library.Domain.Domain.User.Read.Repositories;
using library.Domain.Domain.User.Write.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace library.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]

public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserQueryHandler _queryHandler;

    public UserController(
        UserQueryHandler queryHandler,
        IMediator mediator)
    {
        _queryHandler = queryHandler;
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var id = await _mediator.Send(command);
        
        return Ok(new { Id = id, Message = "Usuário criado com sucesso" });
    }

    #region queries
    
    [Authorize(Roles = "Cliente")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _queryHandler.GetAllUsersAsync();
        return Ok(users);
    }

    #endregion
}