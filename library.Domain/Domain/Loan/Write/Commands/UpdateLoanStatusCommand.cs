using library.Domain.Domain.Infrastructure;
using MediatR;

namespace library.Domain.Domain.Loan.Write.Commands;

public class UpdateLoanStatusCommand : IRequest<Result<Guid>>
{
    public Guid LoanId { get; set; }
    public StatusBook Status { get; set; }

    public UpdateLoanStatusCommand(Guid loanId, StatusBook status)
    {
        LoanId = loanId;
        Status = status;
    }
}
