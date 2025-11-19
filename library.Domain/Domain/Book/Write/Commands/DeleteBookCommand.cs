using library.Domain.Domain.Infrastructure;
using MediatR;

namespace library.Domain.Domain.Book.Write.Commands;

public class DeleteBookCommand  : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}