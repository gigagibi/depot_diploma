using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Depot.Models.Equipments;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class EquipmentModel : IGenericModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    public bool Archived { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }
        
    public EquipmentManufacture EntityManufacture { get; set; }

    [Column("manufacturer")]
    public int EntityManufactureId { get; set; }
        
    public EquipmentType EntityType { get; set; }
        
    [Column("type")]
    public int EntityTypeId { get; set; }
}