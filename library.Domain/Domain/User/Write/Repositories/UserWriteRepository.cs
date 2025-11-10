using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.User.Read.Model;

namespace library.Domain.Domain.User.Write.Repositories;

public interface IUserWriteRepository
{
   Task AddAsync(UserModel user);
   Task SaveChangesAsync();
}


public class UserWriteRepository : IUserWriteRepository
{
   private readonly LibraryDbContext _context;
   
   public UserWriteRepository(LibraryDbContext context)
   {
      _context = context;
   }
   
   public async Task AddAsync(UserModel user)
   {
      await _context.Users.AddAsync(user);
   }

   public Task SaveChangesAsync()
   {
      return _context.SaveChangesAsync();
   }
}
