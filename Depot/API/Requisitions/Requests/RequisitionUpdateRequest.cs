﻿namespace Depot.API.Requisitions.Requests;

public class RequisitionsUpdateRequest
{
    public int RequisitionStatusId { get; set; }
    public int EntityId { get; set; }
    public int EmployeeId { get; set; }
    public int OperatorId { get; set; }
    public DateTime ReservationStartDate { get; set; }
    public DateTime ReservationEndDate { get; set; }
}