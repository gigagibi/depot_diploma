namespace Depot.API.Employees.Requests;

public class EmployeeCreateRequest
{
    public string FIO { get; set; }
    public int DepartmentId { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Link { get; set; }
}