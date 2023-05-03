using Depot.API;
using Depot.API.Authentication.Requests;
using Depot.API.Authentication.Responses;
using Depot.API.Users.Requests;
using Depot.API.Users.Responses;
using Depot.Repositories;

namespace Depot.Services.Users;

public interface IUsersService
{
    Task<LoginResponse?> Authenticate (LoginRequest request);
    Task<UserGetResponse> GetAsync (int id);
    Task<UserGetResponse> GetAsync (string username);
    Task<IEnumerable<UserGetResponse>> GetAllAsync (PaginationParameters? paginationParameters = null);
    Task DeleteAsync (string jwt, int id);
    Task ArchiveAsync (string jwt, int id);
    Task<UserCreateResponse> CreateAsync (UserCreateRequest request);
    Task<UserUpdateResponse> PatchAsync (int id, UserPatchRequest request);
    Task EditPassword(string jwt, string newPassword);
    UserGetResponse GetByEmployeeId (int employeeId);
}