using library.Domain.Domain.Category.Read.Model;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Infrastructure.Repositories.Generic;
using library.Domain.Domain.Interfaces.Write;
using Microsoft.EntityFrameworkCore;

namespace library.Domain.Domain.Category.Write.Repositories;

public interface ICategoryWriteRepository : IGenericWriteRepository<CategoryModel>
{
 
}


public class CategoryWriteRepository : GenericWriteRepository<CategoryModel>, ICategoryWriteRepository
{
    public CategoryWriteRepository(LibraryDbContext context) : base(context)
    {
        
    }
}