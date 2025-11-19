using System.Linq.Expressions;
using library.Domain.Domain.Interfaces.Read;
using Microsoft.EntityFrameworkCore;

namespace library.Domain.Domain.Infrastructure.Repositories.Generic;

public class GenericReadRepository<T> : IGenericReadRepository<T> where T : class
{
    private readonly LibraryDbContext _context;

    public GenericReadRepository(LibraryDbContext context)
    {
        _context = context;
    }
    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public virtual async Task <bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AnyAsync(predicate);
    }
}