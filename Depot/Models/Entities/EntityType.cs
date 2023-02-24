using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Depot.Models.Entities;

public class EntityType : IGenericModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    public bool Archived { get; set; }

    [Column("name")]
    public string Name { get; set; }
        
    [Column("description")]
    public string Description { get; set; }
}