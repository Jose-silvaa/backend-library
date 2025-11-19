

using library.Domain.Domain.Infrastructure;
using library.Domain.Domain.User.Read.Model;
using library.Domain.Domain.User.Read.Repositories;
using library.Domain.Domain.User.Write.Commands;
using library.Domain.Domain.User.Write.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class UserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _repository;
    private readonly IPasswordHasher<UserModel> _passwordHasher;

    public UserCommandHandler(
        IUserReadRepository readRepository,
        IUserWriteRepository repository,
        IPasswordHasher<UserModel> passwordHasher)
    {
        _readRepository = readRepository;
        _passwordHasher =  passwordHasher;
        _repository = repository;
    }   

    public async Task<Result<Guid>> Handle(CreateUserCommand cmd, CancellationToken cancellationToken)
    {
        try
        {
            var emailExists = await _readRepository.GetUserByEmailAsync(cmd.Email);
            
            if (emailExists != null)
            {
                return Result<Guid>.Fail("Este e-mail já está em uso.");
            }
            
            var user = new UserModel(cmd.Email, cmd.Password,  cmd.Role);

            user.Password = _passwordHasher.HashPassword(user, cmd.Password);
            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();
            
            return Result<Guid>.Ok(user.Id);
        }
        catch(Exception e)
        {
            return Result<Guid>.Fail($"Erro ao criar usuário: {e.Message}");
        }
      
        
            
    }
}