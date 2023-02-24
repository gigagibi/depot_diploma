namespace Depot.API.Entities.EntityModels.Responses;

public class EntityModelCreateResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int EntityManufactureId { get; set; }
    public int EntityTypeId { get; set; }
    public string Description { get; set; }
}