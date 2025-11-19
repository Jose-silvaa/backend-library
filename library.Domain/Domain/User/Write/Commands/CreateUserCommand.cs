using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.User.Read.Model;
using MediatR;

namespace library.Domain.Domain.User.Write.Commands;

public record CreateUserCommand(string Email, string Password, UserRole Role) : IRequest<Result<Guid>>;