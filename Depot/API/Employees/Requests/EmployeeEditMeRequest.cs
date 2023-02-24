namespace Depot.API.Employees.Requests;

public class EmployeeEditMeRequest
{
    public string FIO { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Link { get; set; }
}