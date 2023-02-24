namespace Depot.API.Employees.Responses;

public record EmployeeCreateResponse(
    string Id,
    string FIO,
    int DepartmentId,
    string Phone,
    string Email,
    string Link,
    string UserId
);