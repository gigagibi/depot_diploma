
using Depot.Models.Departments;
using Depot.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Depot.Models.Users;

public class Employee : IGenericModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    public bool Archived { get; set; }

    public string FIO { get; set; }
        
    [Column("department_id")]
    public int? DepartmentId { get; set; }   
        
    public Department? Department { get; set; }
        
    [Column("phone")]
    public string Phone { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("link")]
    public string Link { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }
        
    public User User { get; set; }

    [Column("position_id")]
    public int PositionId { get; set; }

    public Position Position { get; set; }
}