using Depot.API;
using Depot.API.Authentication.Requests;
using Depot.API.Users.Requests;
using Depot.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Depot.Controllers.Users;

public interface IUsersController
{
    Task<IActionResult> Authenticate (LoginRequest request);
    Task<IActionResult> GetAsync (int? id = null, 
        string? username = null, 
        int? employeeId = null,
        PaginationParameters? paginationParameters = null,
        bool all = false);
    Task<IActionResult> DeleteAsync (int id);
    Task<IActionResult> ArchiveAsync (int id);
    Task<IActionResult> CreateAsync (UserCreateRequest request);
    Task<IActionResult> UpdateAsync (int id, UserPatchRequest request);
    Task<IActionResult> EditPassword(string newPassword);
}