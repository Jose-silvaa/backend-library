using library.Domain.Domain.Category.Read.Model;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Infrastructure.Repositories.Generic;
using library.Domain.Domain.Interfaces.Read;
using Microsoft.EntityFrameworkCore;

namespace library.Domain.Domain.Category.Read.Repositories;


public interface ICategoryReadRepository : IGenericReadRepository<CategoryModel>
{
  
}

public class CategoryReadRepository : GenericReadRepository<CategoryModel>, ICategoryReadRepository
{
    private readonly LibraryDbContext _context;

    public CategoryReadRepository(LibraryDbContext context) : base(context)
    {
    }

}