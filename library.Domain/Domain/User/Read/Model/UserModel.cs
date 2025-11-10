using System.ComponentModel.DataAnnotations.Schema;
using library.Infrastructure.CQRS;

namespace library.Domain.Domain.User.Read.Model;

public enum UserRole
{
    Cliente = 0,
    Funcionario = 1
}


public class UserModel : BaseModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    
    public UserRole Role { get; private set; } // 👈 novo campo


    public UserModel(string email, string password,  UserRole role = UserRole.Cliente)
    {
        Email = email;
        Password = password;
        Role = role;
    }
}