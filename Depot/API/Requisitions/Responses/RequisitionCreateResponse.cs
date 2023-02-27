namespace Depot.API.Requisitions.Responses;

public class RequisitionCreateResponse
{
    public int Id { get; set; }
    public bool Archived { get; set; }
    public int RequisitionStatusId { get; set; }
    public int EntityId { get; set; }
    public int EmployeeId { get; set; }
    public int OperatorId { get; set; }
    public DateTime LastUpdateTime { get; set; }
    public DateTime ReservationStartDate { get; set; }
    public DateTime ReservationEndDate { get; set; }
}