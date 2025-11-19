using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Infrastructure.Repositories.Generic;
using library.Domain.Domain.Interfaces.Read;
using Microsoft.EntityFrameworkCore;

namespace library.Domain.Domain.Loan.Read.Repositories;


public interface ILoanReadRepository : IGenericReadRepository<LoanModel>
{
    Task<List<LoanModel>> GetLoansByUserIdAsync(Guid userId);
}

public class LoanReadRepository : GenericReadRepository<LoanModel>, ILoanReadRepository
{
    private readonly LibraryDbContext _context;
    
    public LoanReadRepository(LibraryDbContext context) : base(context)
    {
         _context = context;
    }

    public async Task<List<LoanModel>> GetLoansByUserIdAsync(Guid userId)
    {
        return await _context.Set<LoanModel>()
            .Where(loan => loan.UserId == userId)
            .ToListAsync();
    }
}