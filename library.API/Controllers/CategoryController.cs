using library.Domain.Domain.Category.Write.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace library.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]

public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand cmd)
    {
        await _mediator.Send(cmd);
        
        return Ok(new {Message = "Categoria criada com sucesso"});
    }
}