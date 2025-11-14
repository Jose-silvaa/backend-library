using library.Domain.Domain.Category.Read.Model;
using library.Domain.Domain.Infrastructure;
using MediatR;

namespace library.Domain.Domain.Book.Write.Commands;

public record CreateBookCommand(string CategoryId, string Title, string Author, string Publisher, string Description, int  AvailableCopies, int TotalQuantity, string? ImageUrl) :  IRequest<Result<Guid>>;