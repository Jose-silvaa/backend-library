

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
    public required UserModel UserId { get; set; }
    
    public required BookModel BookId { get; set; }
    
    public DateTime LoanDate { get; set; } //Data que o emprestimo do livro foi feito
    
    public DateTime DueDate { get; set; } // Data que foi prevista para devolução
    
    public DateTime? ReturnDate { get; set; } //Date que o livro foi realmente devolvido
    
    public StatusBook Status { get; private set; }


    public LoanModel()
    {
        
    }
    
    public LoanModel(UserModel userId, BookModel bookId, DateTime loanDate, DateTime dueDate, StatusBook status = StatusBook.Ativo)
    {
        UserId = userId;
        BookId = bookId;
        LoanDate = loanDate;
        DueDate = dueDate;
        Status = status;
    }
    
    
}