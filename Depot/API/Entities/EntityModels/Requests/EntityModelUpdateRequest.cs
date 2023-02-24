namespace Depot.API.Entities.EntityModels.Requests;

public class EntityModelUpdateRequest
{
    public string Name { get; set; }
    public int EntityManufactureId { get; set; }
    public int EntityTypeId { get; set; }
    public string Description { get; set; }
}