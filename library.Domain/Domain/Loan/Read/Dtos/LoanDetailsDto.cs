using library.Domain.Domain.Book.Read.Model;
using library.Domain.Domain.User.Read.Model;

namespace library.Domain.Domain.Loan.Read;

public class LoanDetailsDto
{
    public Guid Id { get; set; }
    public UserModel User { get; set; }
    public List<BookModel> Books { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public StatusBook Status { get; set; }
 
    
    
    
}
