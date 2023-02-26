namespace Depot.API.Requisitions.RequisitionStatuses.Responses;

public class RequisitionStatusGetResponse
{
    public int Id { get; set; }
    
    public bool Archived { get; set; }    

    public string Name { get; set; }
}