using System.Linq.Expressions;

namespace library.Domain.Domain.Interfaces.Read;

public interface IGenericReadRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}