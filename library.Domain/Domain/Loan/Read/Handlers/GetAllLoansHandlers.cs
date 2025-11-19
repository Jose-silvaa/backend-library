using library.Domain.Domain.Book.Read.Model;
using library.Domain.Domain.Book.Read.Repositories;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Loan.Read.Repositories;
using library.Domain.Domain.User.Read.Repositories;
using MediatR;

namespace library.Domain.Domain.Loan.Read.Handlers;

public class GetLoansHandler : IRequestHandler<GetLoansQuery, Result<List<LoanDetailsDto>>>
{
    private readonly ILoanReadRepository _loanReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IBookReadRepository _bookReadRepository;

    public GetLoansHandler(
        ILoanReadRepository loanReadRepository,
        IUserReadRepository userReadRepository,
        IBookReadRepository bookReadRepository)
    {
        _loanReadRepository = loanReadRepository;
        _userReadRepository = userReadRepository;
        _bookReadRepository = bookReadRepository;
    }

    public async Task<Result<List<LoanDetailsDto>>> Handle(GetLoansQuery query, CancellationToken ct)
    {
        var loans = query.UserId.HasValue
            ? await _loanReadRepository.GetLoansByUserIdAsync(query.UserId.Value)
            : await _loanReadRepository.GetAllAsync();

        var result = new List<LoanDetailsDto>();

        foreach (var loan in loans)
        {
            var user = await _userReadRepository.GetByIdAsync(loan.UserId);

            var books = new List<BookModel>();
            foreach (var bookId in loan.BookIds)
            {
                var book = await _bookReadRepository.GetByIdAsync(bookId);
                if (book != null) books.Add(book);
            }

            result.Add(new LoanDetailsDto
            {
                Id = loan.Id,
                User = user!,
                Books = books,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                Status = loan.Status
            });
        }
        
        return Result<List<LoanDetailsDto>>.Ok(result);
    }
    
    
    
}