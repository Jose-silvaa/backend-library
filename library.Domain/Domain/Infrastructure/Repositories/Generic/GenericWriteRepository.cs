using library.Domain.Domain.Interfaces.Write;

namespace library.Domain.Domain.Infrastructure.Repositories.Generic;

public class GenericWriteRepository<T>  : IGenericWriteRepository<T> where T : class
{
    private IGenericWriteRepository<T> _genericWriteRepositoryImplementation;
    public virtual Task AddAsync(T entity)
    {
        return _genericWriteRepositoryImplementation.AddAsync(entity);
    }

    public virtual Task UpdateAsync(T entity)
    {
        return _genericWriteRepositoryImplementation.UpdateAsync(entity);
    }

    public virtual Task DeleteAsync(T entity)
    {
        return _genericWriteRepositoryImplementation.DeleteAsync(entity);
    }

    public virtual Task SaveChangesAsync()
    {
        return _genericWriteRepositoryImplementation.SaveChangesAsync();
    }
}