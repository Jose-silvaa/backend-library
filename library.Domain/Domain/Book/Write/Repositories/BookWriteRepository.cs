using library.Domain.Domain.Book.Read.Model;
using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Infrastructure.Repositories.Generic;
using library.Domain.Domain.Interfaces.Write;


namespace library.Domain.Domain.Book.Write.Repositories;


public interface IBookWriteRepository : IGenericWriteRepository<BookModel>
{
    
}


public class BookWriteRepository : GenericWriteRepository<BookModel>, IBookWriteRepository
{
   public BookWriteRepository(LibraryDbContext context) : base(context)
   {
      
   }
}