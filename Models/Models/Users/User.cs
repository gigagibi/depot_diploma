using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Depot.Models.Users;

public class User : IdentityUser<int>, IGenericModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public override int Id { get => base.Id; set => base.Id = value; }

    public bool Archived { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }
        
    public Employee Employee { get; set; }
}