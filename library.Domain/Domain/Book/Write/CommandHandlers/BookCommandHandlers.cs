using library.Domain.Domain.Book.Read.Model;
using library.Domain.Domain.Book.Read.Repositories;
using library.Domain.Domain.Book.Write.Commands;
using library.Domain.Domain.Book.Write.Repositories;
using library.Domain.Domain.Category.Read.Repositories;
using library.Domain.Domain.Infrastructure;
using MediatR;

namespace library.Domain.Domain.Book.Write.CommandHandlers;

public class BookCommandHandlers :
    IRequestHandler<CreateBookCommand, Result<Guid>>,
    IRequestHandler<UpdateBookCommand, Result<Guid>>,
    IRequestHandler<DeleteBookCommand, Result<Guid>>
{

    private readonly IBookWriteRepository _bookWriteRepository;
    private readonly IBookReadRepository _bookReadRepository;
    private readonly ICategoryReadRepository  _categoryRepository;
    
    public BookCommandHandlers(
        IBookReadRepository bookReadRepository,
        ICategoryReadRepository categoryRepository,
        IBookWriteRepository bookWriteRepository)
    {
        _bookReadRepository = bookReadRepository;
        _bookWriteRepository = bookWriteRepository;
        _categoryRepository = categoryRepository;
    }
    
    public async Task<Result<Guid>> Handle(CreateBookCommand cmd, CancellationToken cancellationToken)
    {
        var categoryId = await _categoryRepository.GetByIdAsync(Guid.Parse(cmd.CategoryId));

        if (categoryId == null)
        {
            return Result<Guid>.Fail("Não existe categoria com esse ID");

        }

        if (cmd.AvailableCopies > cmd.TotalQuantity)
        {
            return Result<Guid>.Fail("Exemplares disponíveis não podem execeder a quantidade total de livros");
        }

        var book = new BookModel
        {
            CategoryId = cmd.CategoryId,
            Title = cmd.Title,
            Author = cmd.Author,
            Publisher = cmd.Publisher,
            Description = cmd.Description,
            AvailableCopies = cmd.AvailableCopies,
            TotalQuantity = cmd.TotalQuantity,
            ImageUrl = cmd.ImageUrl ?? "",
        };
            
            
        await _bookWriteRepository.AddAsync(book);
        await _bookWriteRepository.SaveChangesAsync();
        
        return Result<Guid>.Ok(book.Id);
    }

    public async Task<Result<Guid>> Handle(UpdateBookCommand cmd, CancellationToken cancellationToken)
    {
        var book = await _bookReadRepository.GetByIdAsync(cmd.Id);
        
        if (book == null)
            return Result<Guid>.Fail("Livro não encontrado");
        
        if (cmd.CategoryId != null)
        {
            if (!Guid.TryParse(cmd.CategoryId, out Guid categoryGuid))
                return Result<Guid>.Fail("CategoryId inválido");

            var category = await _categoryRepository.GetByIdAsync(categoryGuid);
            if (category == null)
                return Result<Guid>.Fail("Categoria não existe");

            book.CategoryId = cmd.CategoryId;
        }
        
        // TITLE
        if (cmd.Title != null)
            book.Title = cmd.Title;

        // AUTHOR
        if (cmd.Author != null)
            book.Author = cmd.Author;

        // PUBLISHER
        if (cmd.Publisher != null)
            book.Publisher = cmd.Publisher;

        // DESCRIPTION
        if (cmd.Description != null)
            book.Description = cmd.Description;

        // AVAILABLE COPIES
        if (cmd.AvailableCopies.HasValue)
            book.AvailableCopies = cmd.AvailableCopies.Value;

        // TOTAL QUANTITY
        if (cmd.TotalQuantity.HasValue)
            book.TotalQuantity = cmd.TotalQuantity.Value;
        
        //IMAGE URL
        if(cmd.ImageUrl != null)
            book.ImageUrl = cmd.ImageUrl;
        
        if (book.AvailableCopies > book.TotalQuantity)
            return Result<Guid>.Fail("Exemplares disponíveis não podem exceder a quantidade total");

        await _bookWriteRepository.UpdateAsync(book);
        await _bookWriteRepository.SaveChangesAsync();

        return Result<Guid>.Ok(book.Id);
    }

    public async Task<Result<Guid>> Handle(DeleteBookCommand cmd, CancellationToken cancellationToken)
    {
        var book = await _bookReadRepository.GetByIdAsync(cmd.Id);

        if (book == null)
            return Result<Guid>.Fail("Livro não encontrado");
        
        await _bookWriteRepository.DeleteAsync(book);
        
        return Result<Guid>.Ok(book.Id);
    }
}