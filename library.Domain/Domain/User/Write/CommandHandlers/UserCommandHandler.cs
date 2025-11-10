

using library.Domain.Domain.User.Read.Model;
using library.Domain.Domain.User.Write.Commands;
using library.Domain.Domain.User.Write.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class UserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserWriteRepository _repository;
    private readonly IPasswordHasher<UserModel> _passwordHasher;

    public UserCommandHandler(
        IUserWriteRepository repository,
        IPasswordHasher<UserModel> passwordHasher)
    {
        _passwordHasher =  passwordHasher;
        _repository = repository;
    }   

    public async Task<Guid> Handle(CreateUserCommand cmd, CancellationToken cancellationToken)
    {
        var user = new UserModel(cmd.Email, cmd.Password,  cmd.Role);
        
        user.Password = _passwordHasher.HashPassword(user, cmd.Password);
        
        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
        
        return user.Id;
    }
}