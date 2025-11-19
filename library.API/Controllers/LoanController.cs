using library.Domain.Domain.Book.Read.Model;
using library.Domain.Domain.Book.Read.Repositories;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Loan.Read;
using library.Domain.Domain.Loan.Read.Repositories;
using library.Domain.Domain.Loan.Write.Commands;
using library.Domain.Domain.User.Read.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace library.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LoanController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoanController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    
    #region commands
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateLoanCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.Success)
        {
            return BadRequest(new { error =  result.ErrorMessage });
        }
        
        return Ok(result.Data);
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateLoan(Guid id)
    {
        var command = new UpdateLoanStatusCommand(id, StatusBook.Devolvido);
        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Data);
    }
    
    #endregion
    
    #region querys
        
    [HttpGet]
    public async Task<IActionResult> GetAllLoans() 
    { 
        var result = await _mediator.Send(new GetLoansQuery());

        if (!result.Success)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Data);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserLoan(Guid id)
    {
        var loans = await _mediator.Send(new GetLoansQuery
        {
            UserId = id
        });
    
        if (!loans.Success)
            return BadRequest(loans.ErrorMessage);
    
        return Ok(loans);
    }
    
    #endregion
}   