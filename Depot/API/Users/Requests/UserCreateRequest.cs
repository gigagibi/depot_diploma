using Depot.API.Employees.Requests;

namespace Depot.API.Users.Requests;

public class UserCreateRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public EmployeeCreateRequest Employee { get; set; }
}