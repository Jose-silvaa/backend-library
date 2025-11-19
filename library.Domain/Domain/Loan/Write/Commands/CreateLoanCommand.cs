using library.Domain.Domain.Book.Read.Model;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.User.Read.Model;
using MediatR;

namespace library.Domain.Domain.Loan.Write.Commands;

public class CreateLoanCommand : IRequest<Result<LoanModel>>
{
    public required Guid User { get; set; }
    
    public required List<Guid> Books { get; set; }
}