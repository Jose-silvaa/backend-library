using library.Domain.Domain.Book.Read.Model;
using library.Domain.Domain.Book.Read.Repositories;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Loan.Read.Repositories;
using library.Domain.Domain.Loan.Write.Commands;
using library.Domain.Domain.Loan.Write.Repositories;
using library.Domain.Domain.User.Read.Repositories;
using MediatR;

namespace library.Domain.Domain.Loan.Write.CommandHandlers;

public class LoanCommandHandler : IRequestHandler<CreateLoanCommand, Result<LoanModel>>, IRequestHandler<UpdateLoanStatusCommand, Result<Guid>>
{
        
    private readonly ILoanReadRepository _loanReadRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IBookReadRepository _bookReadRepository;
    private readonly ILoanWriteRepository _loanWriteRepository;

    public LoanCommandHandler(
        ILoanReadRepository loanReadRepository,
        IUserReadRepository userReadRepository,
        IBookReadRepository bookReadRepository,
        ILoanWriteRepository loanWriteRepository)
    {
        _loanReadRepository = loanReadRepository;
        _userReadRepository = userReadRepository;
        _bookReadRepository = bookReadRepository;
        _loanWriteRepository = loanWriteRepository;
    }
        
    
    public async Task<Result<LoanModel>> Handle(CreateLoanCommand cmd, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userReadRepository.GetByIdAsync(cmd.User);

            if (user == null)
                return Result<LoanModel>.Fail("Usuário não encontrado");
            
            var books = new List<BookModel>();

            foreach (var id in cmd.Books)
            {
                var book = await _bookReadRepository.GetByIdAsync(id);
                
                if (book == null)
                    return Result<LoanModel>.Fail($"Livro com ID {id} não encontrado.");
                
                books.Add(book);
            }
            
            var loan = new LoanModel
            {
                UserId = user.Id,
                BookIds = books.Select(b => b.Id).ToList(),
                LoanDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                Status = StatusBook.Ativo
            };
            
            await _loanWriteRepository.AddAsync(loan);
            await _loanWriteRepository.SaveChangesAsync();

            return Result<LoanModel>.Ok(loan);
        }
        catch(Exception ex)
        {
            return Result<LoanModel>.Fail($"Erro ao criar empréstimo: {ex.Message}");
        }
        
    }
    
    public async Task<Result<Guid>> Handle(UpdateLoanStatusCommand command, CancellationToken cancellationToken)
    {
        var loan = await _loanReadRepository.GetByIdAsync(command.LoanId);

        if (loan == null)
            return Result<Guid>.Fail("Empréstimo não encontrado");

        if (loan.Status != StatusBook.Devolvido)
        {
            loan.Status = StatusBook.Devolvido;
            loan.ReturnDate = DateTime.UtcNow;

            await _loanWriteRepository.UpdateAsync(loan);
            await _loanWriteRepository.SaveChangesAsync();   
        }

        return Result<Guid>.Ok(loan.Id);
    }
}