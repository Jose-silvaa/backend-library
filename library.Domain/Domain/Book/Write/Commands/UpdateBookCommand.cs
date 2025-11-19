using library.Domain.Domain.Infrastructure;
using MediatR;

namespace library.Domain.Domain.Book.Write.Commands;

public class UpdateBookCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? CategoryId { get; set; }
    public string? Title { get; set; } 
    public string? Author { get; set; } 
    public string? Publisher { get; set; } 
    public string? Description { get; set; }
    public int? AvailableCopies { get; set; }
    public int? TotalQuantity { get; set; }
    public string? ImageUrl { get; set; }
}