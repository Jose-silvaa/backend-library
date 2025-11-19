using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Loan.Read;
using MediatR;

public class GetLoansQuery : IRequest<Result<List<LoanDetailsDto>>>
{
    
    public Guid? UserId { get; set; }
}