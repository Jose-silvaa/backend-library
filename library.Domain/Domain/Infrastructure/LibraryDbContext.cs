using library.Domain.Domain.Book.Read.Model;
using library.Domain.Domain.Category.Read.Model;
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
    public DbSet<CategoryModel> Categories => Set<CategoryModel>();
    public DbSet<LoanModel> Loans => Set<LoanModel>();
    public DbSet<BookModel> Books => Set<BookModel>();
}