namespace Depot.API.Departments.Responses;

public class DepartmentGetResponse
{
    public int Id { get; set; }
    public bool Archived { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Geo { get; set; }
    public string Phone { get; set; }
}