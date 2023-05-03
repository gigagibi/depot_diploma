using Depot.Models.Departments;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Depot.Models.Users;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Depot.Models.Equipments;

public class Equipment : IGenericModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("archived")]
    public bool Archived { get; set; }

    [Column("INV_number")]
    public int InvNumber { get; set; }
        
    [Column("SN_number")]
    public string SnNumber { get; set; }
        
    [Column("BINV_number")]
    public string BinvNumber { get; set; }
        
    [Column("MOT")]
    public string MOT { get; set; }
        
    public Department Department { get; set; }
    
    [Column("department")]
    public int DepartmentId { get; set; }

    [Column("area")]
    public string Area { get; set; }

    [Column("status")]
    public string Status { get; set; }

    [Column("RF")]
    public int? EmployeeId { get; set; }

    public Employee Employee { get; set; }

    [Column("GL_type")]
    public string GlType { get; set; }
    
    [Column("model")]
    public int EntityModelId { get; set; }
    
    public EntityModel EntityModel { get; set; }

    [Column("comment")]
    public string? Comment { get; set; }
    
    [Column("part_of")]
    public bool PartOf { get; set; }
    
    [Column("part_of_field")]
    public int? PartOfField { get; set; }
    
    [Column("father_of")]
    public bool FatherOf { get; set; }
}