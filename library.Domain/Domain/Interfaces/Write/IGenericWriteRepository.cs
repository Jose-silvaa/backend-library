namespace library.Domain.Domain.Interfaces.Write;

public interface IGenericWriteRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SaveChangesAsync();
}