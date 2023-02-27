using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Depot.Models.Entities;
using Depot.Models.Users;

namespace Depot.Models.Requisitions;

public class Requisition : IGenericModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("archived")]
    public bool Archived { get; set; }
    
    public RequisitionStatus RequisitionStatus { get; set; }
    
    [Column("status_id")]
    public int RequisitionStatusId { get; set; }
    
    public Entity Entity { get; set; }

    [Column("entity_id")]
    public int EntityId { get; set; }
    
    public Employee Employee { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }
    
    public Employee Operator { get; set; }

    [Column("operator_id")]
    public int OperatorId { get; set; }

    [Column("last_update")]
    public DateTime LastUpdateTime { get; set; }
    
    [Column("reservation_start")]
    public DateTime ReservationStartDate { get; set; }
    
    [Column("reservation_end")]
    public DateTime ReservationEndDate { get; set; }
    
}