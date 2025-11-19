using library.Infrastructure.CQRS;

namespace library.Domain.Domain.Category.Read.Model;

public class CategoryModel : BaseModel
{
    public required string Name { get; set; }
    public required string Description { get; set; }


    public CategoryModel()
    {
        
    }
    public CategoryModel(string name, string description)
    {
        Name = name;
        Description = description;
    }
}