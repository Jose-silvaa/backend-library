using library.Infrastructure.CQRS;

namespace library.Domain.Domain.User.Read.Model;


public class UserModel : BaseModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}