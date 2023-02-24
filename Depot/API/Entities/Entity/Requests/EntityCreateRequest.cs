namespace Depot.API.Entities.Entity.Requests;

public class EntityCreateRequest
{
    public int? InvNumber { get; set; }
    public string SnNumber { get; set; }
    public string BinvNumber { get; set; }
    public string MOT { get; set; }
    public int DepartmentId { get; set; }
    public string Area { get; set; }
    public string Status { get; set; }
    public string GlType { get; set; }
    public int EntityModelId { get; set; }
    public string Comment { get; set; }
    public bool PartOf { get; set; } = false;
    public int? PartOfField { get; set; } = null;
    public bool FatherOf { get; set; } = false;
}