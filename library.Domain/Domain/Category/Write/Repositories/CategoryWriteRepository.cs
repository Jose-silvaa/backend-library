using library.Domain.Domain.Category.Read.Model;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Infrastructure.Repositories.Generic;
using library.Domain.Domain.Interfaces.Write;

namespace library.Domain.Domain.Category.Write.Repositories;

public interface ICategoryWriteRepository : IGenericWriteRepository<CategoryModel>
{
    new Task AddAsync(CategoryModel category);
    new Task SaveChangesAsync();
}


public class CategoryWriteRepository : GenericWriteRepository<CategoryModel>, ICategoryWriteRepository
{
    private readonly LibraryDbContext _context;

    public CategoryWriteRepository(LibraryDbContext context)
    {
        _context = context;
    }
    
    public override async Task AddAsync(CategoryModel category)
    {
        await _context.Categories.AddAsync(category);
    }
    
    public override Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}