using library.Domain.Domain.Book.Read.Model;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Infrastructure.Repositories.Generic;
using library.Domain.Domain.Interfaces.Read;
using Microsoft.EntityFrameworkCore;

namespace library.Domain.Domain.Book.Read.Repositories;

public interface IBookReadRepository : IGenericReadRepository<BookModel>
{
    new Task<BookModel?> GetByIdAsync(Guid id);
    
    new Task<IEnumerable<BookModel>> GetAllAsync();
}

public class BookReadRepository : GenericReadRepository<BookModel>, IBookReadRepository
{
    private readonly LibraryDbContext _context;

    public BookReadRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public override async Task<BookModel?> GetByIdAsync(Guid id)
    {
        return await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public override async Task<IEnumerable<BookModel>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }
    
    
}