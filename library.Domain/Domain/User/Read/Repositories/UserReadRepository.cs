using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.Infrastructure.Repositories.Generic;
using library.Domain.Domain.Interfaces.Read;
using library.Domain.Domain.User.Read.Model;
using Microsoft.EntityFrameworkCore;

namespace library.Domain.Domain.User.Read.Repositories;

public interface IUserReadRepository : IGenericReadRepository<UserModel>
{
    Task<UserModel?> GetUserByEmailAsync(string email);
}

public class UserReadRepository : GenericReadRepository<UserModel>, IUserReadRepository
{
    private readonly LibraryDbContext _context;
 
    public UserReadRepository(
        LibraryDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<UserModel?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
}