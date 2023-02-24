namespace Depot.API.Departments.Requests;

public class DepartmentUpdateRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Geo { get; set; }
    public string Phone { get; set; }
}