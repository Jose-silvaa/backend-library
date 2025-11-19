using library.Domain.Domain.Category.Write.Repositories;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Infrastructure.Repositories.Generic;
using library.Domain.Domain.Interfaces.Write;
using Microsoft.EntityFrameworkCore;

namespace library.Domain.Domain.Loan.Write.Repositories;


public interface ILoanWriteRepository : IGenericWriteRepository<LoanModel>
{
    
}

public class LoanWriteRepository : GenericWriteRepository<LoanModel>, ILoanWriteRepository
{
    public LoanWriteRepository(LibraryDbContext context) :  base(context)
    {
        
    }
}