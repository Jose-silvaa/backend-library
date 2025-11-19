using library.Domain.Domain.Infrastructure;
using MediatR;

namespace library.Domain.Domain.User.Write.Commands;

public record LoginCommand(string Email, string Password) : IRequest<Result<string>>;
