using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Depot.Models.Departments;

public class Department : IGenericModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    public bool Archived { get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; }
        
    [Column("description")]
    public string Description { get; set; }
        
    [Column("geo")]
    public string Geo { get; set; }

    [Column("phone")]
    public string Phone { get; set; }
}