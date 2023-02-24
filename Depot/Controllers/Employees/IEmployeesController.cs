using Depot.API;
using Depot.API.Employees.Requests;
using Depot.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Employees;

public interface IEmployeesController
{
    Task<IActionResult> GetAsync (int? id = null,
        int? userId = null,
        PaginationParameters? paginationParameters = null, bool all = false);
    Task<IActionResult> DeleteAsync (int id);
    Task<IActionResult> ArchiveAsync (int id);
    Task<IActionResult> UpdateAsync (int id, EmployeeUpdateRequest request);
    Task<IActionResult> EditMeAsync (EmployeeEditMeRequest request);
}