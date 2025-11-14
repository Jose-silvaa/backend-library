using System.Linq.Expressions;
using library.Domain.Domain.Interfaces.Read;

namespace library.Domain.Domain.Infrastructure.Repositories.Generic;

public class GenericReadRepository<T> : IGenericReadRepository<T> where T : class
{
    private IGenericReadRepository<T> _genericReadRepositoryImplementation;
    public virtual Task<T?> GetByIdAsync(Guid id)
    {
        return _genericReadRepositoryImplementation.GetByIdAsync(id);
    }

    public virtual Task<IEnumerable<T>> GetAllAsync()
    {
        return _genericReadRepositoryImplementation.GetAllAsync();
    }

    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return _genericReadRepositoryImplementation.FindAsync(predicate);
    }

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return _genericReadRepositoryImplementation.ExistsAsync(predicate);
    }
}