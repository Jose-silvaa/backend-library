using library.Domain.Domain.Book.Read.Repositories;
using library.Domain.Domain.Book.Write.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace library.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]

public class BookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IBookReadRepository  _bookReadRepository;

    public BookController(
        IBookReadRepository bookReadRepository,
        IMediator mediator)
    {
        _bookReadRepository = bookReadRepository;
        _mediator = mediator;
    }
    
    #region commands

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(new { error = result.ErrorMessage});
        
        return Ok(new {Message = "Livro criado com sucesso"});
    }

    [HttpPatch("{id:guid}")]
    [Route("Update")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookCommand command)
    {
        command.Id = id;
        
        var result = await _mediator.Send(command);
        
        if(!result.Success)
            return BadRequest(new { error = result.ErrorMessage});
        
        return Ok(new {Message = "Livro atualizado com sucesso"});
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteBookCommand { Id = id };
        
        var result = await _mediator.Send(command);
        
        if(!result.Success)
            return BadRequest(new { error = result.ErrorMessage});
        
        return Ok(new {Message = result.Success});
    }
    
    #endregion
    
    #region queries

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookReadRepository.GetAllAsync();
        return Ok(books);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var book = await _bookReadRepository.GetByIdAsync(id);
        
        return Ok(book);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetNumberBooks()
    {
        var books = await _bookReadRepository.GetTotalNumberOfBooks();
        return Ok(books);
    }
    
    
    #endregion
    
}