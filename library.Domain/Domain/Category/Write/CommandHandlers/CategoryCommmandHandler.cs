using library.Domain.Domain.Category.Read.Model;
using library.Domain.Domain.Category.Write.Commands;
using library.Domain.Domain.Category.Write.Repositories;
using MediatR;

public class CategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>  
{
    private readonly ICategoryWriteRepository _repository;

    public CategoryCommandHandler(
        ICategoryWriteRepository repository)
    {
        _repository = repository;
    }
    public async Task<Guid> Handle(CreateCategoryCommand cmd, CancellationToken cancellationToken)
    {
        var category = new CategoryModel
        {
            Name = cmd.Name,
            Description = cmd.Description,
        };

        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();
        
        return category.Id;

    }
}