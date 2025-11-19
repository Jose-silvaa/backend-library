using MediatR;

namespace library.Domain.Domain.Category.Write.Commands;

public record CreateCategoryCommand(string Name, string Description) :  IRequest<Guid>;