

using System.ComponentModel;
using library.Domain.Domain.Book.Read.Model;
using library.Domain.Domain.User.Read.Model;
using library.Infrastructure.CQRS;

public enum StatusBook
{
    [Description("Ativo")]
    Ativo,
    
    [Description("Atrasado")]
    Atrasado,
    
    [Description("Devolvido")]
    Devolvido
}

public class LoanModel : BaseModel
{
    public required Guid UserId { get; set; }

    public required List<Guid> BookIds { get; set; } = new();

    public DateTime LoanDate { get; set; } = DateTime.UtcNow;//Data que o emprestimo do livro foi feito
    
    public DateTime DueDate { get; set; } // Data que foi prevista para devolução
    
    public DateTime? ReturnDate { get; set; } //Date que o livro foi realmente devolvido
    
    public StatusBook Status { get; set; }


    public LoanModel()
    {
        
    }
    
    public LoanModel(Guid userId, List<Guid> booksIds, DateTime dueDate, StatusBook status = StatusBook.Ativo)
    {
        UserId = userId;
        BookIds = booksIds; ;
        DueDate = dueDate;
        Status = status;
    }
    
}