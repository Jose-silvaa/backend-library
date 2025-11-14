using library.Domain.Domain.Book.Read.Model;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Infrastructure.Repositories.Generic;
using library.Domain.Domain.Interfaces.Write;

namespace library.Domain.Domain.Book.Write.Repositories;


public interface IBookWriteRepository : IGenericWriteRepository<BookModel>
{
    new Task AddAsync(BookModel book);
    
    new Task SaveChangesAsync();
    
    new Task UpdateAsync(BookModel book);
}


public class BookWriteRepository : GenericWriteRepository<BookModel>, IBookWriteRepository
{
    private readonly LibraryDbContext _context;

    public BookWriteRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public override async Task AddAsync(BookModel book)
    {
        await _context.Books.AddAsync(book);
    }

    public override Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public override async Task UpdateAsync(BookModel book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }
}