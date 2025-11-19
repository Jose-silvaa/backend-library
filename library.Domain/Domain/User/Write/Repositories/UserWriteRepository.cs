using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Infrastructure.Repositories.Generic;
using library.Domain.Domain.Interfaces.Write;
using library.Domain.Domain.User.Read.Model;

namespace library.Domain.Domain.User.Write.Repositories;

public interface IUserWriteRepository : IGenericWriteRepository<UserModel>
{
}


public class UserWriteRepository : GenericWriteRepository<UserModel>,IUserWriteRepository
{
   private readonly LibraryDbContext _context;
   
   public UserWriteRepository(LibraryDbContext context): base(context)
   {
   }
   
}
