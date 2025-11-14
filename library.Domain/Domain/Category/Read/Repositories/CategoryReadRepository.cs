using library.Domain.Domain.Category.Read.Model;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Infrastructure.Repositories.Generic;
using library.Domain.Domain.Interfaces.Read;
using Microsoft.EntityFrameworkCore;

namespace library.Domain.Domain.Category.Read.Repositories;


public interface ICategoryReadRepository : IGenericReadRepository<CategoryModel>
{
    new Task<CategoryModel?> GetByIdAsync(Guid id);
    new Task<IEnumerable<CategoryModel>> GetAllAsync();
}

public class CategoryReadRepository : GenericReadRepository<CategoryModel>, ICategoryReadRepository
{
    private readonly LibraryDbContext _context;

    public CategoryReadRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public override async Task<CategoryModel?> GetByIdAsync(Guid id)
    {
        return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public override async Task<IEnumerable<CategoryModel>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

}