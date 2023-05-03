using Depot.API;
using Depot.API.Employees.Requests;
using Depot.API.Employees.Responses;
using Depot.API.Users.Responses;
using Depot.Repositories;

namespace Depot.Services.Employees;

public interface IEmployeesService
{
    Task<EmployeeGetResponse> GetAsync (int id);
    Task<IEnumerable<EmployeeGetResponse>> GetAllAsync (PaginationParameters? paginationParameters = null);
    EmployeeGetResponse GetByUserId (int userId);
    Task DeleteAsync (string jwt, int id);
    Task ArchiveAsync (string jwt, int id);
    Task<EmployeeUpdateResponse> UpdateAsync (int id, EmployeeUpdateRequest request);
    Task<EmployeeUpdateResponse> EditMeAsync (string jwt, EmployeeEditMeRequest request);
}