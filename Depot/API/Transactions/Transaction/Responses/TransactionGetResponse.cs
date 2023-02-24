namespace Depot.API.Transactions.Transaction.Responses;

public class TransactionGetResponse
{
    public int Id { get; set; }
    
    public bool Archived { get; set; }
    
    public int EntityId { get; set; }
    
    public int AuthorId { get; set; }
    
    public DateTime Date { get; set; }
        
    public int TransactionTypeId { get; set; }
    
    public string? TransactionComment { get; set; }
        
    public bool EntityArchived { get; set; }

    public int InvNumber { get; set; }
        
    public string SnNumber { get; set; }
        
    public string BinvNumber { get; set; }
        
    public string MOT { get; set; }
        
    public int DepartmentId { get; set; }

    public string Area { get; set; }

    public string Status { get; set; }

    public int? EmployeeId { get; set; }

    public string GlType { get; set; }
    
    public int EntityModelId { get; set; }

    public string? Comment { get; set; }
    
    public bool PartOf { get; set; }
    
    public int? PartOfField { get; set; }
    
    public bool FatherOf { get; set; }
}