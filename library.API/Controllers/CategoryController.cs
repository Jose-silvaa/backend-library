using library.Domain.Domain.Category.Read.Repositories;
using library.Domain.Domain.Category.Write.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace library.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]

public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICategoryReadRepository  _categoryRepository;

    public CategoryController(
        ICategoryReadRepository categoryRepository,
        IMediator mediator)
    {
        _categoryRepository = categoryRepository;
        _mediator = mediator;
    }

    #region commands

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand cmd)
    {
        await _mediator.Send(cmd);
        
        return Ok(new {Message = "Categoria criada com sucesso"});
    }
    
    #endregion
    

    #region queries
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        
        return Ok(category);
    }

    #endregion
}