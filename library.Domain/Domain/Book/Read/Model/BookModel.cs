using library.Domain.Domain.Category.Read.Model;
using library.Infrastructure.CQRS;

namespace library.Domain.Domain.Book.Read.Model;

public class BookModel : BaseModel
{
    public required CategoryModel CategoryId { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string Publisher { get; set; }
    public required string Description { get; set; }
    public required int AvailableCopies { get; set; }
    public required int TotalQuantity { get; set; }

    public BookModel()
    {
        
    }
    
    public BookModel(CategoryModel categoryId, string title, string author, string publisher, string description, int availableCopies, int totalQuantity)
    {
        CategoryId = categoryId;
        Title = title;
        Author = author;
        Publisher = publisher;
        Description = description;
        AvailableCopies = availableCopies;
        TotalQuantity = totalQuantity;
    }
    
    
}