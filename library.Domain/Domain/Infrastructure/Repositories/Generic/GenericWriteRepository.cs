using library.Domain.Domain.Interfaces.Write;


namespace library.Domain.Domain.Infrastructure.Repositories.Generic;

public class GenericWriteRepository<T>  : IGenericWriteRepository<T> where T : class
{
    private readonly LibraryDbContext _context;

    public GenericWriteRepository(LibraryDbContext context)
    {
        _context = context;
    }
    public virtual Task AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        return Task.CompletedTask;
    }

    public virtual Task UpdateAsync(T entity)
    {
       _context.Set<T>().Update(entity);
       return Task.CompletedTask;   
    }

    public virtual Task DeleteAsync(T entity)
    {
       _context.Set<T>().Remove(entity);
       return Task.CompletedTask;
    }

    public virtual Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}