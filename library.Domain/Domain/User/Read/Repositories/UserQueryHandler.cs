using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.User.Read.Model;
using Microsoft.EntityFrameworkCore;

namespace library.Domain.Domain.User.Read.Repositories;

public class UserQueryHandler
{
    private readonly LibraryDbContext _context;
 
    public UserQueryHandler(LibraryDbContext context)
    {
        _context = context;
    }
    
    public async Task<UserModel?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }
    
    public async Task<UserModel?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
    
    public async Task<List<UserModel>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users.ToListAsync(cancellationToken);
    }
}