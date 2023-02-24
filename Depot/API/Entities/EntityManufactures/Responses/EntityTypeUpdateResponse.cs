namespace Depot.API.Entities.EntityManufactures.Responses;

public class EntityManufactureUpdateResponse
{
    public int Id { get; set; }
    public bool Archived { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Description { get; set; }
}