using System.ComponentModel.DataAnnotations.Schema;

namespace Depot.Models.Requisitions;

public class RequisitionStatus : IGenericModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("archived")]
    public bool Archived { get; set; }    
    
    [Column("name")]
    public string Name { get; set; }
}