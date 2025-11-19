using library.Domain.Domain.Book.Read.Model;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Infrastructure.Repositories.Generic;
using library.Domain.Domain.Interfaces.Read;
using Microsoft.EntityFrameworkCore;

namespace library.Domain.Domain.Book.Read.Repositories;

public interface IBookReadRepository : IGenericReadRepository<BookModel>
{
    Task<int> GetTotalNumberOfBooks();
}

public class BookReadRepository : GenericReadRepository<BookModel>, IBookReadRepository
{
    private readonly LibraryDbContext _context;

    public BookReadRepository(LibraryDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<int> GetTotalNumberOfBooks()
    {
        return await _context.Books.CountAsync();
    }
    
}