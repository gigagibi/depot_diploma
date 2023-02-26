namespace Depot.API.Requisitions.RequisitionStatuses.Responses;

public class RequisitionStatusUpdateResponse
{
    public int Id { get; set; }
    
    public bool Archived { get; set; }    

    public string Name { get; set; }
}