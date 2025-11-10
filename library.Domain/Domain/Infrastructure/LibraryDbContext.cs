using library.Domain.Domain.User.Read.Model;
using Microsoft.EntityFrameworkCore;

namespace library.Domain.Domain.Infrastructure;

public class LibraryDbContext : DbContext
{
    
    public LibraryDbContext()
    {
    }
    
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=librarydb;Username=postgres;Password=macae123");
        }
    }
    
    public DbSet<UserModel> Users => Set<UserModel>();
}