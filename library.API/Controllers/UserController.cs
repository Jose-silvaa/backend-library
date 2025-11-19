using library.Domain.Domain.User.Read.Repositories;
using library.Domain.Domain.User.Write.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace library.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]

public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserReadRepository _readRepository;

    public UserController(
        IUserReadRepository readRepository,
        IMediator mediator)
    {
        _readRepository = readRepository;
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.Success)
        {
            return BadRequest(new
            {
                error =  result.ErrorMessage
            });
        }
        
        return Ok(new {  Message = "Usuário criado com sucesso", userId = result.Data });
    }

    #region queries
    
    [Authorize(Roles = "Cliente")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _readRepository.GetAllAsync();
        return Ok(users);
    }

    #endregion
}