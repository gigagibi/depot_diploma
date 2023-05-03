namespace Depot.API.Positions.Responses;

public class PositionCreateResponse
{
    public int Id { get; set; }
    public bool Archived { get; set; }
    public string Name { get; set; }
}