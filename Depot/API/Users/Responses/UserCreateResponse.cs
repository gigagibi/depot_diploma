using Depot.API.Employees.Requests;
using Depot.API.Employees.Responses;

namespace Depot.API.Users.Responses;

public class UserCreateResponse
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public EmployeeCreateResponse Employee { get; set; }
}