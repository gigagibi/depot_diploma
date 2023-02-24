namespace Depot.API.Employees.Responses;

public class EmployeeUpdateResponse
{
    public string Id { get; set; }
    public string FIO { get; set; }
    public int DepartmentId { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Link { get; set; }
}