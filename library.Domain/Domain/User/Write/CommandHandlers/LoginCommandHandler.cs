using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using library.Domain.Domain.User.Read.Model;
using library.Domain.Domain.User.Read.Repositories;
using library.Domain.Domain.User.Write.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace library.Domain.Domain.User.Write.CommandHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly UserQueryHandler _readRepository;
    private readonly IPasswordHasher<UserModel> _passwordHasher;
    private readonly IConfiguration _config;

    public LoginCommandHandler(
        IPasswordHasher<UserModel> passwordHasher,
        UserQueryHandler readRepository,
        IConfiguration config)
    {
        _readRepository = readRepository;
        _passwordHasher = passwordHasher;
        _config = config;
    }
    
    public async Task<string> Handle(LoginCommand cmd, CancellationToken cancellationToken)
    {
        var user = await _readRepository.GetUserByEmailAsync(cmd.Email, cancellationToken);

        if (user == null)
            throw new UnauthorizedAccessException("Email ou senha inválidos.");

        var verify = _passwordHasher.VerifyHashedPassword(user, user.Password, cmd.Password);

        if (verify != PasswordVerificationResult.Success)
            throw new UnauthorizedAccessException("Email ou senha inválidos.");
        
        return GenerateJwtToken(user);
    }
    
    private string GenerateJwtToken(UserModel user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}