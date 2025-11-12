namespace library.Infrastructure.CQRS;

public interface IBaseModel
{
    Guid Id { get; set; }

    DateTime CreatedAt { get; set; }
}

public class BaseModel : IBaseModel
{
    public virtual Guid Id { get; set; }
    
    public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}